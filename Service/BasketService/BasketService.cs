
using Microsoft.AspNetCore.Http.HttpResults;
using PostrgreSqlApi.Model;
using ShoppingCartApi.Repositories.Abstract;

namespace ShoppingCartApi.Service.BasketService
{
    public class BasketService : IBasketService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BasketService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

    

        public async Task<IEnumerable<BasketModel>> GetAllBaskets()
        {
            return await _unitOfWork.BasketRepository.GetAll();
        }



        public async Task<BasketModel> GetBasketById(int id)
        {
            return await _unitOfWork.BasketRepository.GetById(id);
        }



        public async Task<string> AddBasket(BasketModel basketModel)
        {
            await _unitOfWork.BasketRepository.Add(basketModel);
            return "Basket added successfully";
        }



        public async Task<string> UpdateBasket(int id, BasketModel basketModel)
        {
            var existingBasket = await _unitOfWork.BasketRepository.GetById(id);
            if (existingBasket == null)
            {
                return "Basket not found";
            }
            existingBasket.CustomerID = basketModel.CustomerID;
            existingBasket.ProductID = basketModel.ProductID;
            existingBasket.Quantity = basketModel.Quantity;
            existingBasket.TotalPrice = basketModel.TotalPrice;
            existingBasket.AddedDate = basketModel.AddedDate;
            existingBasket.Status = basketModel.Status;

            await _unitOfWork.BasketRepository.Update(existingBasket);
            return "Basket updated successfully";
        }



        public async Task<string> DeleteBasket(int id)
        {
            var result = await _unitOfWork.BasketRepository.GetById(id);
            
            if (result == null )
            {
                return "Basket not found";
            }
            await _unitOfWork.BasketRepository.Delete(id);
            return "Basket deleted successfully";
        }
    }
}

