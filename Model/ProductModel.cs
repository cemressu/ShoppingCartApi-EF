using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCartApi.Model
{
    [Table(name: "product")]
    public class ProductModel : BaseEntity
    {
        [Required]
        [Column(name: "name")]
        public required string Name { get; set; }


        [Column(name: "price")]
        public decimal Price { get; set; }


        [Column(name: "stock")]
        public int Stock { get; set; }


        [Column(name: "category")]
        public required string Category { get; set; }


        [Column(name: "brand")]
        public required string Brand { get; set; }

    }
}
