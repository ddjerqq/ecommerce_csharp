﻿using System;

namespace ecommerce.Core.Models
{
    public class Product
    {
        
        public int     Id          { get; set; }
        public string  Name        { get; set; }
        public string  Description { get; set; }
        public decimal Price       { get; set; }
        public int     Stock       { get; set; }
        public DateTime CreatedAt
        {
            get
            {
                return GioId.GetCreationDate(Id);
            }
        }
    }
}