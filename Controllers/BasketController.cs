using Microsoft.AspNetCore.Mvc;
using PostrgreSqlApi.Model;
using ShoppingCartApi.Repositories.Abstract;
using ShoppingCartApi.Service.BasketService;


namespace ShoppingCartApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }



        [HttpGet("GetAllBaskets")]
        public async Task<IEnumerable<BasketModel>> GetAllBaskets()
        {
            IEnumerable<BasketModel> baskets = await _basketService.GetAllBaskets();

            return baskets;
        }

        

       
        [HttpGet("{id}/GetBasketById")]
        public async Task<ActionResult<BasketModel>> GetBasketById(int id)
        {
            var basket = await _basketService.GetBasketById(id);
            if (basket == null)
            {
                return NotFound($"Basket ID = {id} not found");
            }
            return Ok(basket);
        }




        [HttpPost("AddBasket")]
        public async Task<ActionResult<BasketModel>> AddBasket([FromBody] BasketModel basketModel)
        {
            await _basketService.AddBasket(basketModel);
            return Ok("Basket added successfully");
        }




        [HttpPut("{id}/UpdateBasket")]
        public async Task<ActionResult<string>> UpdateBasket(int id, [FromBody] BasketModel basketModel)
        {
            var result = await _basketService.UpdateBasket(id, basketModel);
            if (result == "Basket not found")
            {
                return NotFound($"Basket ID = {id} not found");
            }
            return Ok("Basket updated successfully");
        }



        //delete metoduna o id db de yoksa not found döndür lazım
        [HttpDelete("{id}/DeleteBasket")]
        public async Task<ActionResult<string>> DeleteBasket(int id)
        {
            var result = await _basketService.DeleteBasket(id);
            if (result == null )
            {
                return NotFound($"Basket ID = {id} not found");
            }
            return Ok("Basket deleted successfully");
        }

    }
}


