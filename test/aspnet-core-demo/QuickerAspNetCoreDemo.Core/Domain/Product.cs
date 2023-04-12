using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Quicker.Domain.Entities.Auditing;

namespace QuickerAspNetCoreDemo.Core.Domain
{
    [Table("AppProducts")]
    public class Product : FullAuditedEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public float? Price { get; set; }

        public Product()
        {
            
        }

        public Product(string name, float? price = null)
        {
            Name = name;
            Price = price;
        }
    }
}