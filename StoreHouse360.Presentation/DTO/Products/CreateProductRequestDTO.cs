﻿using StoreHouse360.Application.Commands.Products;
using StoreHouse360.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.DTO.Products
{
    public class CreateProductRequestDTO : IMapFrom<CreateProductCommand>
    {
        [Required] public string Name { get; set; }
        [Required] public int CategoryId { get; set; }
        [Required] public int ManufacturerId { get; set; }
        [Required] public int CountryOriginId { get; set; }
        [Required] public int UnitId { get; set; }
        [Required] public string Barcode { get; set; }
        [Required] public double Price { get; set; }
        [Required] public int CurrencyId { get; set; }
        public int? MinimumLevel { get; set; }
    }
}
