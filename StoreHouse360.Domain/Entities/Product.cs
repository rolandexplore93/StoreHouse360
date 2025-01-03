﻿namespace StoreHouse360.Domain.Entities
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public int UnitId { get; set; }
        public Unit Unit { get; set; }
        public string Barcode { get; set; }
        public double Price { get; set; }

        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public int MinimumLevel { get; set; }
        public bool HasMinimumLevel => MinimumLevel > 0;
    }
}
