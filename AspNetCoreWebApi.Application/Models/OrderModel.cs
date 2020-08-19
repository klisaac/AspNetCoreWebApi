﻿using System.Collections.Generic;
namespace AspNetCoreWebApi.Application.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public decimal GrandTotal { get; set; }
        public IEnumerable<OrderItemModel> OrderItems { get; set; }
    }
}
