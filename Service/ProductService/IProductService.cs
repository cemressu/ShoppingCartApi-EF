using PostrgreSqlApi.Model;
using ShoppingCartApi.Model;

namespace ShoppingCartApi.Service.ProductService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> GetAllProducts();
        Task<ProductModel> GetProductById(int id);
        Task<string> AddProduct(ProductModel productModel);
        Task<string> UpdateProduct(int id, ProductModel productModel);
        Task<string> DeleteProduct(int id);
    }
}

