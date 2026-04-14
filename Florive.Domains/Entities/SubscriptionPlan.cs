namespace Florive.Domains.Entities
{
    public class SubscriptionPlan
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int DeliveriesCount { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}