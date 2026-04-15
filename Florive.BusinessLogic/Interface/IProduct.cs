using Florive.Domains.Entities;
using Florive.Domains.Models;
using Florive.Domains.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florive.BusinessLogic.Interface
{
    public interface IProduct
    {
        ResponseMsg GetAllProductsAction();
        ResponseMsg GetProductByIdAction(int id);
        ResponseMsg CreateProductAction(ProductDTO product);
        ResponseMsg UpdateProductAction(int id, ProductDTO product);
        ResponseMsg DeleteProductAction(int id);
    }
}
