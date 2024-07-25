using gerenciar_pedidos.Models;

public interface IOrderRepository{

    Task<IEnumerable<OrderDto>> GetAllOrders();
    
    Task<Order> GetByOrderId(int id);

    Task<Order> CreateOrder(Order order);

    // Task<Order> Update(Order order);

    Task DeleteOrder(int id);
}