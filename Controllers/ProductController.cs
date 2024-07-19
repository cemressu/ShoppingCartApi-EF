using Microsoft.AspNetCore.Mvc;
using PostrgreSqlApi.Model;
using ShoppingCartApi.Model;
using ShoppingCartApi.Service.BasketService;
using ShoppingCartApi.Service.ProductService;

namespace ShoppingCartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }



        [HttpGet("GetAllProducts")]
        public async Task<IEnumerable<ProductModel>> GetAllProducts()
        {
            IEnumerable<ProductModel> products = await _productService.GetAllProducts();

            return products;
        }


        [HttpGet("{id}/GetProductById")]
        public async Task<ActionResult<ProductModel>> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound($"Product ID = {id} not found");
            }
            return Ok(product);
        }


        [HttpPost("AddProduct")]
        public async Task<ActionResult<ProductModel>> AddProduct([FromBody] ProductModel productModel)
        {
            await _productService.AddProduct(productModel);
            return Ok("Product added successfully");
        }



        [HttpPut("{id}/UpdateProduct")]
        public async Task<ActionResult<string>> UpdateProduct(int id, [FromBody] ProductModel productModel)
        {
            var result = await _productService.UpdateProduct(id, productModel);
            if (result == "Product not found")
            {
                return NotFound($"Product ID = {id} not found");
            }
            return Ok("Product updated successfully");
        }



        [HttpDelete("{id}/DeleteProduct")]
        public async Task<ActionResult<string>> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProduct(id);
            if (result == null)
            {
                return NotFound($"Product ID = {id} not found");
            }
            return Ok("Product deleted successfully");
        }


    }
}
