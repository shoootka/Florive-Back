using Florive.BusinessLogic.Core.SubscriptionPlans;
using Florive.BusinessLogic.Interface;
using Florive.DataAccess;
using Florive.Domains.Models;
using Florive.Domains.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florive.BusinessLogic.Functions.SubscriptionPlans
{
    public class SubscriptionPlanFlow : SubscriptionPlanAction, ISubscriptionPlan
    {
        public SubscriptionPlanFlow(AppDbContext context) : base(context)
        {
        }

        public ResponseMsg GetAllPlansAction()
        {
            return ExecuteGetAllPlansAction();
        }

        public ResponseMsg GetPlanByIdAction(int id)
        {
            return GetPlanDataByIdAction(id);
        }

        public ResponseMsg CreatePlanAction(SubscriptionPlanDTO plan)
        {
            return ExecutePlanCreateAction(plan);
        }

        public ResponseMsg UpdatePlanAction(int id, SubscriptionPlanDTO plan)
        {
            return ExecutePlanUpdateAction(id, plan);
        }

        public ResponseMsg DeletePlanAction(int id)
        {
            return ExecutePlanDeleteAction(id);
        }
    }
}
