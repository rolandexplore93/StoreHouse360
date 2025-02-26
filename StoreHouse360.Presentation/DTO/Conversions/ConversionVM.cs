using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Entities;
using StoreHouse360.DTO.Common;
using StoreHouse360.DTO.Products;
using StoreHouse360.DTO.StoragePlaces;
using StoreHouse360.DTO.Warehouses;

namespace StoreHouse360.DTO.Conversions
{
    public class ConversionVM : IViewModel, IMapFrom<Conversion>
    {
        public int Id { get; set; }

        public WarehouseVM FromWarehouse { get; set; }

        public WarehouseVM ToWarehouse { get; set; }

        public ProductJoinedVM FromProduct { get; set; }

        public ProductJoinedVM ToProduct { get; set; }

        public StoragePlaceVM FromPlace { get; set; }

        public StoragePlaceVM ToPlace { get; set; }

        public int FromQuantity { get; set; }

        public int ToQuantity { get; set; }

        public string Note { get; set; }

        public int ExportInvoiceId { get; set; }

        public int ImportInvoiceId { get; set; }
    }
}
