namespace OrderApi.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string? EventName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? MaxSeats { get; set; }
    }
}
