using EquipmentService.BLL.Models;
using EquipmentService.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentService.BLL.Interfaces
{
    public interface IOrderManager
    {
        Task CreateOrder(Order order);
        Task UpdateOrder(Order order);
        Task DeleteOrder(int id);
        Task<bool> CancelOrder(int id);
        Task<bool> SuccessOrder(int id);
        Task<bool> FailOrder(int id);

    }
}
