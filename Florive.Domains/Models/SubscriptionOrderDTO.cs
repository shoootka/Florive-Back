using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florive.Domains.Models
{
    public class SubscriptionOrderDTO
    {
        public int Id { get; set; }
        public int SubscriptionPlanId { get; set; }
        public int FirstFlowerId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Frequency { get; set; }
        public DateTime FirstDeliveryDate { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
    }
}
