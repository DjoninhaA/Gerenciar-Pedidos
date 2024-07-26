using Microsoft.AspNetCore.Mvc;
using Moq;
using gerenciar_pedidos.Controllers;
using gerenciar_pedidos.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace gerenciar_pedidos_testes
{
    public class OrderControllerTests
    {
        [Fact]
        public async Task GetAllOrders_Returns_ListOfOrders()
        {
            // Arrange
            var data = new List<Order>
            {
                new Order
                {
                    OrderId = 1,
                    OrderDate = DateTime.Now,
                    IsClosed = false,
                    OrderDetails = new List<OrderDetails>
                    {
                        new OrderDetails
                        {
                            ProductId = 1,
                            Product = new Product
                            {
                                ProductId = 1,
                                ProductName = "Chocolate",
                                Price = 2.0m
                            },
                            Quantity = 3,
                            UnitPrice = 2.0m
                        }
                    }
                },
                new Order
                {
                    OrderId = 2,
                    OrderDate = DateTime.Now,
                    IsClosed = true,
                    OrderDetails = new List<OrderDetails>
                    {
                        new OrderDetails
                        {
                            ProductId = 2,
                            Product = new Product
                            {
                                ProductId = 2,
                                ProductName = "Coffee",
                                Price = 5.0m
                            },
                            Quantity = 1,
                            UnitPrice = 5.0m
                        }
                    }
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Order>>();
            mockSet.As<IQueryable<Order>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Order>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Order>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Order>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.Orders).Returns(mockSet.Object);

            var controller = new OrderController(mockContext.Object);

            // Act
            var result = await controller.GetAllOrders(isClosed: false, pageNumber: 1, pageSize: 10);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<OrderDto>>(okResult.Value);
            Assert.NotEmpty(returnValue);
        }
    }
}
