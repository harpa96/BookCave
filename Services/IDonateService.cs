using BookCave.Models.InputModels;

namespace BookCave.Services
{
    public interface IDonateService
    {
        void ProcessDonate(DonateInputModel donate);
    }
}

