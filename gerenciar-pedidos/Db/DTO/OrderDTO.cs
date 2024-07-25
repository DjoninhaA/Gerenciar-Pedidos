

public class OrderDto
{
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }

        public bool IsClosed { get; set; }

        public List<OrderDetailsDto> OrderDetails { get; set; } = new List<OrderDetailsDto>();

        public decimal TotalPrice { get; set; }

}



