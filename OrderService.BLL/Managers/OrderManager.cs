using OrderService.BLL.Interfaces;
using OrderService.BLL.Models;
using OrderService.DAL.Entities;
using OrderService.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BLL.Managers
{
    public class OrderManager : IOrderManager
    {
        private readonly IRepository<Order> repository;

        public OrderManager(IRepository<Order> repository)
        {
            this.repository = repository;
        }

        public async Task CreateOrder(Order order)
        {
            await repository.Insert(order);
        }

        public async Task UpdateOrder(Order order)
        {
            await repository.Update(order);
        }

        public async Task DeleteOrder(int id)
        {
            var entity = await repository.Get(id);
            await repository.Delete(entity);
        }

        public async Task<bool> CancelOrder(int id)
        {
            var entity = await repository.Get(id);
            if (entity == null)
                return false;
            entity.Status = "Cancelled";
            await repository.Update(entity);
            return true;
        }

        public async Task<Order> SuccessOrder(int id)
        {
            var entity = await repository.Get(id);
            if (entity == null)
                return null;
            entity.Status = "Success";
            await repository.Update(entity);
            if (entity.OrderType == "Return")
            {
                entity.Quantity *= -1;
            }
            return entity;
        }
        public async Task<bool> FailOrder(int id)
        {
            var entity = await repository.Get(id);
            if (entity == null)
                return false;
            entity.Status = "Fail";
            await repository.Update(entity);
            return true;
        }
    }
}
