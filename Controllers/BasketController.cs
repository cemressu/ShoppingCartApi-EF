using Microsoft.AspNetCore.Mvc;
using PostrgreSqlApi.Model;

namespace ShoppingCartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly DbContext _dbContext;

        public BasketController(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // DEPENDENCY INJECTION kullandık



       
        [HttpGet("GetAllBaskets")]
        public IActionResult GetAllBaskets()
        {
            var baskets = _dbContext.GetAllBaskets();

            return Ok(baskets);
            //tüm sepetleri almak için
        }



       
        [HttpGet("{id}/GetBasketById")]
        public IActionResult GetBasketById(int id)
        {
            var basket = _dbContext.GetBasketById(id);

            if (basket==null)
            {
                return NotFound("Basket not found.");
            }
            return Ok(basket);
        }
        //belirtilen id'ye sahip sepetin bilgilerini verir



       
        [HttpPost("AddBasket")]
        public IActionResult AddBasket([FromBody] BasketModel basket)
        {
            _dbContext.AddBasket(basket);

            return Ok("Basket added successfully."); //sepeti ekler ve ekranda bunu döndürür
                                           //yeni sepet eklemek için
        }



        
        [HttpPut("{id}/UpdateBasket")]
        public IActionResult UpdateBasket(int id, [FromBody] BasketModel basket)
        {
            var updated = _dbContext.UpdateBasket(id, basket);

            if (updated == null || updated == "0")
            {
                return NotFound("Basket not found."); //güncelleme basarisiz olursa bunu
            }
            return Ok("Basket updated succesfully."); //basarili olursa bunu döndürür
                                         //istenilen id deki bir sepeti güncellemek için
        }




        [HttpDelete("{id}/DeleteBasket")]
        public IActionResult DeleteBasket(int id)
        {
            var deleted = _dbContext.DeleteBasket(id);

            if (deleted == null || deleted == "0")
            {
                return NotFound("Basket not found."); //silme işlemi başarısız olursa bunu
            }
            return Ok("Basket deleted succesfully."); //başarılı olurssa bunu döndürür
                                         //istenilen id deki bir sepeti silmek için
        }
    }
}


