using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Florive.Domains.Entities.Products;

public class DescriptionAdvanced
{
    [Key]
    public int Id { get; set; }

    public double? Width { get; set; }

    public double? Height { get; set; }

    public int DescriptionId { get; set; }

    public ProductDescriptionData Description { get; set; } = null!;
}