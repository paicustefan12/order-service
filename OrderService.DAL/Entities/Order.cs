﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentService.DAL.Entities
{
    public class Order : BaseEntity
    {
        public string Status { get; set; }
        public string OrderType { get; set; }
        public int EquipmentId { get; set; }
        public int Quantity { get; set; }
    }
}