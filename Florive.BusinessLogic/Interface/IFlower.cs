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
    public interface IFlower
    {
        ResponseMsg GetAllFlowersAction();
        ResponseMsg GetFlowerByIdAction(int id);
        ResponseMsg CreateFlowerAction(FlowerDTO flower);
        ResponseMsg UpdateFlowerAction(int id, FlowerDTO flower);
        ResponseMsg DeleteFlowerAction(int id);
    }
}
