using BookCave.Models.ViewModels;

namespace BookCave.Services
{
    public interface IOrderService
    {
        //void ProcessOrder(CheckoutViewModel specialorder);
        void SendOrderEmail(CheckoutViewModel specialorder);
    }
}
