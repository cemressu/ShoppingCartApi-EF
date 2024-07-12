using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostrgreSqlApi.Model
{
    [Table(name: "basket")]
    public class BasketModel
    {

        [Key]
        [Required]
        [Column(name: "id")]
        public int ID { get; set; }


        [Required]
        [Column(name: "customer_id")]
        public int CustomerID { get; set; }


        [Required]
        [Column(name: "product_id")]
        public int ProductID { get; set; }


        [Column(name: "quantity")]
        public int Quantity { get; set; }


        [Column(name: "total_price")]
        public decimal TotalPrice { get; set; }


        [Column(name: "added_date")]
        public DateTime AddedDate { get; set; }


        [Column(name: "status")]
        public required string Status { get; set; }

    }
}


