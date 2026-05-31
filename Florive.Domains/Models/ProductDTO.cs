using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florive.Domains.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public string? Category { get; set; }

        public string? Image { get; set; }

        public List<string> Images { get; set; } = new();

        public string? ShortDescription { get; set; }

        public string? FullDescription { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
    }
}
