﻿using System.Collections.Generic;
using AspNetCoreWebApi.Application.Models;
namespace AspNetCoreWebApi.Application.Responses
{
    public class OrderResponse
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurName { get; set; }
        public IEnumerable<OrderModel> Orders { get; set; }
    }
}
