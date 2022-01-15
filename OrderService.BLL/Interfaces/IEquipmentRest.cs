using OrderService.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BLL.Interfaces
{
    public interface IEquipmentRest
    {
        Task<bool> SendUpdateStock(SuccessOrderModel successOrderModel);
    }
}
