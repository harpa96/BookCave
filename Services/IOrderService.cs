using BookCave.Models.ViewModels;

namespace BookCave.Services
{
    public interface IOrderService
    {
        void ProcessOrder(CheckoutViewModel order);
        void SendOrderEmail(CheckoutViewModel order);
    }
}
