using gerenciar_pedidos.Models;

public class OrderDetailsDto{

    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

}