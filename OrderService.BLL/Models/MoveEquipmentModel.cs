﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BLL.Models
{
    public class MoveEquipmentModel
    {
        public int EquipmentId { get;set;}
        public int DepartmentId { get;set;} 
        public int Quantity { get;set;}
    }
}
