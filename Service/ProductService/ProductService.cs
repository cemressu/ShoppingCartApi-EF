using PostrgreSqlApi.Model;
using ShoppingCartApi.Model;
using ShoppingCartApi.Repositories.Abstract;

namespace ShoppingCartApi.Service.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<ProductModel>> GetAllProducts()
        {
            return await _unitOfWork.ProductRepository.GetAll();
        }



        public async Task<ProductModel> GetProductById(int id)
        {
            return await _unitOfWork.ProductRepository.GetById(id);
        }




        public async Task<string> AddProduct(ProductModel productModel)
        {
            await _unitOfWork.ProductRepository.Add(productModel);
            return "Product added successfully";
        }



        public async Task<string> UpdateProduct(int id, ProductModel productModel)
        {
            var existingProduct = await _unitOfWork.ProductRepository.GetById(id);
            if (existingProduct == null)
            {
                return "Product not found";
            }
            existingProduct.Name = productModel.Name;
            existingProduct.Price = productModel.Price;
            existingProduct.Stock = productModel.Stock;
            existingProduct.Category = productModel.Category;
            existingProduct.Brand = productModel.Brand;

            await _unitOfWork.ProductRepository.Update(existingProduct);
            return "Product updated successfully";
        }



        public async Task<string> DeleteProduct(int id)
        {
            var result = await _unitOfWork.ProductRepository.GetById(id);

            if (result == null)
            {
                return "Product not found";
            }
            await _unitOfWork.ProductRepository.Delete(id);
            return "Product deleted successfully";
        }

       



       
    }
}
