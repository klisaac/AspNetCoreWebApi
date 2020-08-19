﻿using AspNetCoreWebApi.Application.Models;

namespace AspNetCoreWebApi.Application.Responses
{
    public class ProductResponse
    {
        public int ProductId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? UnitPrice { get; set; }
        public CategoryModel Category { get; set; }
    }
}
