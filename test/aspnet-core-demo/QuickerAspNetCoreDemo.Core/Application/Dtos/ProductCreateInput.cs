using System.ComponentModel.DataAnnotations;
using Quicker.Application.Services.Dto;
using Quicker.AutoMapper;
using QuickerAspNetCoreDemo.Core.Domain;

namespace QuickerAspNetCoreDemo.Core.Application.Dtos
{
    [AutoMap(typeof(Product))]
    public class ProductCreateInput
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public float? Price { get; set; }
    }
}