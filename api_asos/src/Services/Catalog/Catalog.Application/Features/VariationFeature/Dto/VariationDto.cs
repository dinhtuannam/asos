using Catalog.Application.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.VariationFeature.Dto
{
    public class VariationDto:BaseDto<Guid>
    {
        public Guid Id { get; set; }
        public int QtyDisplay { get; set; } = 0;
        public int QtyInStock { get; set; } = 0;
        public decimal Stock { get; set; } = 0;
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Variation, VariationDto>();
            }
        }
    }
}
