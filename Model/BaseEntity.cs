using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCartApi.Model
{
    public class BaseEntity
    {

        [Key]
        [Required]
        [Column(name: "id")]
        public int ID { get; set; }
    }
}
