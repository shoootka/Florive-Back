using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Florive.Domains.Entities.Products;

public class ProductDescriptionData
{
    [Key]
    public int Id { get; set; }

    [StringLength(500)]
    public string ShortDescription { get; set; } = string.Empty;

    public string FullDescription { get; set; } = string.Empty;

    public int ProductId { get; set; }
    public ProductData Product { get; set; } = null!;

    public DescriptionAdvanced DescriptionAdvanced { get; set; } = null!;
}
