
using Catalog.Application.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.ProductItemFeature.Dto
{
    public class ProductItemDto:BaseDto<Guid>
    {

        public decimal OriginalPrice { get; set; } = 0;
        public decimal SalePrice { get; set; } = 0;
        public bool IsSale { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<ProductItem, ProductItemDto>();
            }
        }

    }
}
