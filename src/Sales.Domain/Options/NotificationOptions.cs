namespace Sales.Domain.Options
{
    public interface INotificationOptions
    {
        string Url { get; set; }
    }

    public class NotificationOptions : INotificationOptions
    {
        public string Url { get; set; }
    }
}
