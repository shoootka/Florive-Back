using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Florive.Domains.Entities.Products;

public class ProductImgData
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(500)]
    public string Url { get; set; } = string.Empty;

    public bool IsMain { get; set; }

    public int ProductId { get; set; }
    public ProductData Product { get; set; } = null!;
}