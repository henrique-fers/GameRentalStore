using GameRentalStore.Controllers.Models;
using GameRentalStore.Controllers.Models.Views;

namespace GameRentalStore.Services.Interfaces
{
    public interface IPromotionService
    {
        public Task<ResultViewModel<List<Promotion>>> GetAllPromotion();
        Task<ResultViewModel<Promotion>> GetPromotionByDescription(string description);
        public Task<ResultViewModel<Promotion>> GetPromotionById(int id);
        Task<ResultViewModel<List<Promotion>>> GetAllValidsPromotion();
        public Task CreatePromotion(Promotion promotion);
        public Task<ResultViewModel<Promotion>> DeletePromotion(int id);
        public Task UpdatePromotion(Promotion promotion);
    }
}


