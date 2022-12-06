namespace OrderApi.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int EventId { get; set; }
        public int UserId{ get; set; }
        public DateTime DateTime { get; set; }
    }
}
