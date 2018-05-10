using BookCave.Models.InputModels;

namespace BookCave.Services
{
    public interface ISpecialOrderService
    {
        void ProcessSpecialOrder(SpecialOrderInputModel specialorder);
    }
}
