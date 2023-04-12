using Quicker.Application.Services.Dto;
using Quicker.AutoMapper;
using QuickerAspNetCoreDemo.Core.Domain;

namespace QuickerAspNetCoreDemo.Core.Application.Dtos
{
    [AutoMap(typeof(Product))]
    public class ProductDto : EntityDto
    {
        public string Name { get; set; }

        public float Price { get; set; }
    }
}
