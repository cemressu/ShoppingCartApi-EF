using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCartApi.Model
{
    [Table(name:"order")]
    public class OrderModel : BaseEntity
    {
        [Required]
        [Column(name: "customer_id")]
        public int CustomerID { get; set; }


        [Required]
        [Column(name: "product_id")]
        public int ProductID { get; set; }


        [Column(name: "order_date")]
        public DateTime OrderDate { get; set; }


        [Column(name: "total_price")]
        public decimal TotalPrice { get; set; }


        [Column(name: "status")]
        public required string Status { get; set; }


        [Column(name: "cargo_company")]
        public required string CargoCompany { get; set; }


        [Column(name: "quantity")]
        public int Quantity { get; set; }
    }
}
