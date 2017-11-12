﻿namespace Products.API.Models
{
    using System;
    public class ProductResponse
    {
        public int ProductId { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        public DateTime LastPorcharse { get; set; }

        public string Image { get; set; }

        public double Stock { get; set; }

        public string Reamarks { get; set; }

    }
}