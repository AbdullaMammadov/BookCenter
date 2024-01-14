using FinalProject.ViewModels;

namespace FinalProject.Services
{
    public interface IBasketService
    {
        int GetCount();
        void IncreaseBook(int id);
        void IncreaseProduct(int id);
        void DecreaseBook(int id);
        void DecreaseProduct(int id);
        void DeleteProduct(int id);
        void DeleteBook(int id);
        List<BasketVM> GetBasket();


    }
}