using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florive.Domains.Models
{
    public class UserDTO
    {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Phone { get; set; }
            public bool IsActive { get; set; }
    }
}
