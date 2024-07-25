using gerenciar_pedidos.Models;
using Microsoft.EntityFrameworkCore;


public class OrderRepository : IOrderRepository {

    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context) {
        _context = context;
    }

    public async Task<Order> CreateOrder(Order order)
    {
       _context.Orders.Add(order);
       await _context.SaveChangesAsync();
       return order;
    }

    public async Task<Order> UpdateOrder(Order order){

        _context.Entry(order).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return order;
    }


    public async Task<Order> GetByOrderId(int id){

        return await _context.Orders
        .Include(order => order.OrderDetails)
        .FirstOrDefaultAsync(order => order.OrderId == id);
     }



    public async Task<IEnumerable<OrderDto>> GetAllOrders(){

        var orders = await _context.Orders
        .Include(order => order.OrderDetails)
        .ThenInclude(details => details.Product)
        .ToListAsync();

        var orderDtos = orders.Select(order => new OrderDto{

            OrderId = order.OrderId,
            OrderDate = order.OrderDate,
            IsClosed = order.IsClosed,
            OrderDetails = order.OrderDetails.Select(details => new OrderDetailsDto{

                ProductId = details.ProductId,
                ProductName = details.Product.ProductName,
                Quantity = details.Quantity,
                UnitPrice = details.UnitPrice
                
            }).ToList(),
            TotalPrice = order.OrderDetails.Sum(details => details.Quantity * details.UnitPrice)
            
        });
        return orderDtos;
    }    


    public async Task DeleteOrder(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if(order != null) 
         _context.OrderDetails.RemoveRange(order.OrderDetails);

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }


}

    





    
    