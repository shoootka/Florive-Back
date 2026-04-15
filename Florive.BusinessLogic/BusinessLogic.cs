using Florive.BusinessLogic.Functions.Products;
using Florive.BusinessLogic.Interface;

namespace Florive.BusinessLogic
{
    public class BusinessLogic
    {
        public IProduct GetProductActions()
        {
            return new ProductFunction();
        }

        // TODO: добавить остальные 
    }
}