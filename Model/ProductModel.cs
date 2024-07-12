using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostrgreSqlApi.Model
{
    [Table(name:"product")]
    public class ProductModel
    {
        [Key]
        [Required]
        [Column(name: "id")]
        public required int ID { get; set; }


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