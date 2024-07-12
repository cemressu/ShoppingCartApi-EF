using Microsoft.AspNetCore.Mvc;
using PostrgreSqlApi.Model;


namespace ShoppingCartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DbContext _dbContext;

        public ProductController(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        //dependency injection uygulaması



       
        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            var products = _dbContext.GetAllProducts();

            return Ok(products);
        }
        //tüm ürün kayıtlarını alır ve listeler



        
        [HttpGet("{id}/GetProductById")]
        public IActionResult GetProductById(int id)
        {
            var product = _dbContext.GetProductById(id);

            if (product==null)
            {
                return NotFound("Product not found.");
            }
            return Ok(product);
        }
        //belirtilen id'ye sahip ürünün bilgilerini verir



       
        [HttpPost("AddProduct")]
        public IActionResult AddProduct([FromBody] ProductModel product)
        {
            _dbContext.AddProduct(product);

            return Ok("Product added successfully.");
        }
        //yeni ürün ekler
        


       
        [HttpPut("{id}/UpdateProduct")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductModel product)
        {
            var updated = _dbContext.UpdateProduct(id, product);

            if (updated == null || updated == "0")
            {
                return NotFound("Product not found.");
            }
            return Ok("Product updated succesfully.");
        }
        //belirtilen id deki ürünü günceller



       
        [HttpDelete("{id}/DeleteProduct")]
        public IActionResult DeleteProduct(int id)
        {
            var deleted = _dbContext.DeleteProduct(id);

            if (deleted == null || deleted == "0")
            {
                return NotFound("Product not found.");
            }
            return Ok("Product deleted successfully.");
        }
        //belirtilen id deki ürünü siler
    }
}
