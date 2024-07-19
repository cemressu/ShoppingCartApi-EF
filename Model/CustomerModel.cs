using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCartApi.Model
{
    [Table(name: "customer")]
    public class CustomerModel : BaseEntity
    {
        [Required]
        [Column(name: "name")]
        public required string Name { get; set; }


        [Required]
        [Column(name: "surname")]
        public required string Surname { get; set; }


        [Column(name: "phone_number")]
        public required string PhoneNumber { get; set; }


        [Column(name: "address")]
        public required string Address { get; set; }


        [Column(name: "email")]
        public required string Email { get; set; }

    }
}
