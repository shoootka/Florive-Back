using Florive.BusinessLogic.Functions.Products;
using Florive.BusinessLogic.Functions.Users;
using Florive.BusinessLogic.Interface;
using Florive.DataAccess;

namespace Florive.BusinessLogic
{
    public class BusinessLogic
    {
        private readonly AppDbContext _context;

        public BusinessLogic(AppDbContext context)
        {
            _context = context;
        }

        public IProduct GetProductActions()
        {
            return new ProductFunction(_context);  
        }

        public IUser GetUserActions() 
        {
            return new UserFunction(_context);
        }

        // TODO: добавить остальные 
    }
}