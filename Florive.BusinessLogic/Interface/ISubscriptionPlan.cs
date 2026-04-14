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
    public interface ISubscriptionPlan
    {
        ResponseMsg GetAllPlansAction();
        ResponseMsg GetPlanByIdAction(int id);
        ResponseMsg CreatePlanAction(SubscriptionPlanDTO plan);
        ResponseMsg UpdatePlanAction(int id, SubscriptionPlanDTO plan);
        ResponseMsg DeletePlanAction(int id);
    }
}
