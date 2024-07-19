using PostrgreSqlApi.Model;

namespace ShoppingCartApi.Service.BasketService
{
    public interface IBasketService
    {
        Task<IEnumerable<BasketModel>> GetAllBaskets();
        Task<BasketModel> GetBasketById(int id);
        Task<string> AddBasket(BasketModel basketModel);
        Task<string> UpdateBasket(int id, BasketModel basketModel);
        Task<string> DeleteBasket(int id);
    }
}

