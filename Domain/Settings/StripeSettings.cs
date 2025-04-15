namespace Domain.Settings
{
    public class StripeSettings
    {
        public string ApiKey { get; set; }
        public string SuccessUrl { get; set; }
        public string CancelUrl { get; set; }
        public string WebhookSecret { get; set; }
    }
}
