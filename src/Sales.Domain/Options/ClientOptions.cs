namespace Sales.Domain.Options
{
    public class ClientOptions : IClientOptions
    {
        public string WebhookReturnUrl { get; set; }
        public string WebhookCancelUrl { get; set; }
        public string NotificactionUrl { get; set; }
        public int OrderDaysToExpire { get; set; }
    }

    public interface IClientOptions
    {
        public string WebhookReturnUrl { get; set; }
        public string WebhookCancelUrl { get; set; }
        public string NotificactionUrl { get; set; }
        public int OrderDaysToExpire { get; set; }
    }
}
