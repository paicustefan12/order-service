using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BLL.Models
{
    public class OrderModel
    {
        public string MessageType { get; set; }
        public string OrderType { get; set; }
        public int? EquipmentId { get; set; }
        public int? Quantity { get; set; }
        public int? OrderId { get; set; }
    }
}
