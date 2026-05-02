using Florive.BusinessLogic.Core.Products;
using Florive.BusinessLogic.Interface;
using Florive.DataAccess;
using Florive.Domains.Models;
using Florive.Domains.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florive.BusinessLogic.Functions.Products
{
    public class ProductFlow : ProductAction, IProduct
    {
        public ProductFlow(AppDbContext context) : base(context)
        {
        }

        public ResponseMsg GetAllProductsAction()
        {
            return ExecuteGetAllProductsAction();
        }

        public ResponseMsg GetProductByIdAction(int id)
        {
            return GetProductDataByIdAction(id);
        }

        public ResponseMsg CreateProductAction(ProductDTO product)
        {
            return ExecuteProductCreateAction(product);
        }

        public ResponseMsg UpdateProductAction(int id, ProductDTO product)
        {
            return ExecuteProductUpdateAction(id, product);
        }

        public ResponseMsg DeleteProductAction(int id)
        {
            return ExecuteProductDeleteAction(id);
        }
    }
}
