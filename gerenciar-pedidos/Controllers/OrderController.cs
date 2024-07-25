
using gerenciar_pedidos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace gerenciar_pedidos.Controllers
{
    [ApiController]
    [Route("api/orders")]

    public class OrderController : ControllerBase
    {

        private readonly AppDbContext  _context;

        public OrderController(AppDbContext context){

            _context = context;
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder(OrderDto orderDto){

            if (orderDto == null || orderDto.OrderDetails == null || !orderDto.OrderDetails.Any())
            {
                return BadRequest("O pedido não pode ser nulo e deve possuir itens.");
            }

            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                IsClosed = false
            };

            decimal totalPrice = 0;

            foreach (var detailDto in orderDto.OrderDetails)
            {
        
                var product = await _context.Products
                    .FirstOrDefaultAsync(p => p.ProductName == detailDto.ProductName);

                if (product == null)
                {
                    return BadRequest($" O produto '{detailDto.ProductName}' não foi encontrado.");
                }

                var orderDetail = new OrderDetails{
                    ProductId = product.ProductId,
                    Quantity = detailDto.Quantity,
                    UnitPrice = product.Price 
                };

                order.OrderDetails.Add(orderDetail);

                totalPrice += orderDetail.Quantity * orderDetail.UnitPrice;
            }

                order.TotalPrice = totalPrice;

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetOrderById), new { id = order.OrderId }, order);
        }


       [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateOrder(int id, UpdateOrderDetailsDto updateOrderDetailsDto)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound("O ID do Pedido não encontrado.");
            }

            if (order.IsClosed)
            {
                return BadRequest("O pedido está fechado, não é possível fazer alterações.");
            }

            // Remove itens que não estão no DTO
            var productNamesToUpdate = updateOrderDetailsDto.OrderDetails.Select(dto => dto.ProductName).ToList();
            var itemsToRemove = order.OrderDetails
                .Where(od => !productNamesToUpdate.Contains(
                    _context.Products
                        .Where(p => p.ProductId == od.ProductId)
                        .Select(p => p.ProductName)
                        .FirstOrDefault()))
                .ToList();

            _context.OrderDetails.RemoveRange(itemsToRemove);

            foreach (var detailDto in updateOrderDetailsDto.OrderDetails)
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(p => p.ProductName == detailDto.ProductName);

                if (product == null)
                {
                    return BadRequest($"Produto com nome '{detailDto.ProductName}' não encontrado.");
                }

                var existingDetail = order.OrderDetails
                    .FirstOrDefault(od => od.ProductId == product.ProductId);

                if (existingDetail != null)
                {
                    if (detailDto.Quantity > 0)
                    {
                        existingDetail.Quantity = detailDto.Quantity;
                    }
                    else
                    {
                        _context.OrderDetails.Remove(existingDetail);
                    }
                }
                else if (detailDto.Quantity > 0)
                {
                    order.OrderDetails.Add(new OrderDetails
                    {
                        ProductId = product.ProductId,
                        Quantity = detailDto.Quantity,
                        OrderId = order.OrderId,
                        UnitPrice = product.Price
                    });
                }
            }

            order.TotalPrice = order.OrderDetails.Sum(orderDetails => orderDetails.Quantity * orderDetails .UnitPrice);

            await _context.SaveChangesAsync();

            return Ok(order);
        }




         [HttpGet("/{id}")]
        public async Task<IActionResult> GetOrderById(int id){

            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return BadRequest("ID não encontrado.");
            }

            var orderDto = new OrderDto
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                IsClosed = order.IsClosed,
                OrderDetails = order.OrderDetails.Select(details => new OrderDetailsDto
                {
                    ProductId = details.ProductId,
                    ProductName = details.Product.ProductName,
                    Quantity = details.Quantity,
                }).ToList(),
                TotalPrice = order.OrderDetails.Sum(details => details.Quantity * details.UnitPrice)
            };

                return Ok(orderDto);    
        }


        [HttpGet]
        public async Task<IActionResult> GetAllOrders(){
            
            var orders = await _context.Orders
            .Include(order => order.OrderDetails)
            .ThenInclude(details => details.Product)
            .ToListAsync();

            var orderDtos = orders.Select(order => new OrderDto
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                IsClosed = order.IsClosed,
                OrderDetails = order.OrderDetails.Select(details => new OrderDetailsDto
                {
                    ProductId = details.ProductId,
                    ProductName = details.Product.ProductName,
                    Quantity = details.Quantity,
                    UnitPrice = details.UnitPrice
                }).ToList(),
                TotalPrice = order.OrderDetails.Sum(details => details.Quantity * details.UnitPrice)
            }).ToList();

            return Ok(orderDtos);
        }

        [HttpPatch("close/{id}")]
        public async Task<IActionResult> CloseOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails) 
                .FirstOrDefaultAsync(order => order.OrderId == id);

            if (order == null)
            {
                return BadRequest("ID do pedido não encontrado.");
            }

            if (!order.OrderDetails.Any())
            {
                return BadRequest("O pedido não pode ser fechado porque não há produtos cadastrados.");
            }

            order.IsClosed = true;

            _context.Entry(order)
                .State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Order = order,
                Message = $"O pedido {id} foi fechado com sucesso."
            });
        }



        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteOrder(int id){
            
            var order = await _context.Orders.FindAsync(id);
            if(order != null) 
            _context.OrderDetails.RemoveRange(order.OrderDetails);


            if(order == null)
            {
                return BadRequest($"ID {id} não encontrado.");
            }

            _context.OrderDetails.RemoveRange(order.OrderDetails);

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok("Pedido excluído com sucesso.");
        }

    }
}

