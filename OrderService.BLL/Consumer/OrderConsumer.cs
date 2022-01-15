using OrderService.BLL.Interfaces;
using OrderService.DAL.Entities;
using MassTransit;
using OrderService.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BLL.Consumer
{
    public class OrderConsumer : IConsumer<OrderModel>
    {
        private readonly IOrderManager orderManager;
        private readonly IEquipmentRest equipmentRest;

        public OrderConsumer(IOrderManager orderManager,
            IEquipmentRest equipmentRest)
        {
            this.orderManager = orderManager;
            this.equipmentRest = equipmentRest;
        }

        public async Task Consume(ConsumeContext<OrderModel> context)
        {
            var obj = context.Message;
            switch (obj.MessageType)
            {
                case "Create":  
                    await Create(obj);
                    break;

                case "Delete":
                    await Delete(obj);
                    break;

                case "Cancel":
                    await Cancel(obj);
                    break;

                case "Success":
                    await Success(obj);
                    break;

                case "Fail":
                    await Fail(obj);
                    break;
            }
        }

        private async Task Create(OrderModel orderModel)
        {
            var order = new Order
            {
                EquipmentId = orderModel.EquipmentId.GetValueOrDefault(),
                OrderType = orderModel.OrderType,
                Quantity = orderModel.Quantity.GetValueOrDefault(),
            };

            await orderManager.CreateOrder(order);
        }

        private async Task Delete(OrderModel orderModel)
        {
            await orderManager.DeleteOrder(orderModel.OrderId.GetValueOrDefault());
        }

        private async Task Cancel(OrderModel orderModel)
        {
            await orderManager.CancelOrder(orderModel.OrderId.GetValueOrDefault());
        }

        private async Task Success(OrderModel orderModel)
        {
            var order = await orderManager.SuccessOrder(orderModel.OrderId.GetValueOrDefault());
            if (order == null)
                return;
            var response = await equipmentRest.SendUpdateStock(new SuccessOrderModel
            {
                EquipmentId = order.EquipmentId,
                Quantity = order.Quantity
            });
        }

        private async Task Fail(OrderModel orderModel)
        {
            await orderManager.FailOrder(orderModel.OrderId.GetValueOrDefault());
        }

    }
}
