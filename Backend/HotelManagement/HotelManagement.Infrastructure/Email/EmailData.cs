namespace HotelManagement.Infrastructure.Email
{
    public class EmailData
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime ReceivedDate { get; set; }
        public string From { get; set; }
    }
}
