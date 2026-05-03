using Florive.BusinessLogic.Functions.Products;
using Florive.BusinessLogic.Functions.SubscriptionOrders;
using Florive.BusinessLogic.Functions.SubscriptionPlans;
using Florive.BusinessLogic.Functions.Users;
using Florive.BusinessLogic.Functions.Auth;
using Florive.BusinessLogic.Functions.Sessions; 
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
            return new ProductFlow(_context);  
        }

        public IUser GetUserActions() 
        {
            return new UserFlow(_context);
        }

        public ISubscriptionPlan GetSubscriptionPlanActions()
        {
            return new SubscriptionPlanFlow(_context);
        }

        public ISubscriptionOrder GetSubscriptionOrderActions()
        {
            return new SubscriptionOrderFlow(_context);
        }

        public IAuth GetAuthFlow()
        {
            return new AuthFlow(_context);
        }

        public ISession GetSessionActions()
        {
            return new SessionFlow(_context);
        }
    }
}