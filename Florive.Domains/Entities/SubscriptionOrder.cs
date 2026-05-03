namespace Florive.Domains.Entities
{
    public class SubscriptionOrder
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SubscriptionPlanId { get; set; }
        public int FirstFlowerId { get; set; }
        //данные для доставки(может отличаться от данных самого юзера):
        public string Name { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Frequency { get; set; } = string.Empty;

        public DateTime FirstDeliveryDate { get; set; }

        public string Comment { get; set; } = string.Empty;

        public string Status { get; set; } = "New";
        public DateTime CreatedAt { get; set; }
    }
}