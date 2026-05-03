using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florive.Domains.Entities
{
    public class UserSession
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string SessionKey { get; set; }
        public DateTime ExpiresAt { get; set; }

        public User User { get; set; }
    }
}
