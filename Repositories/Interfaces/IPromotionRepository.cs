using GameRentalStore.Controllers.Models;

namespace GameRentalStore.Repositories.Interfaces
{
    public interface IPromotionRepository 
    {
        public Task<List<Promotion>> GetAllPromotion();
        Task<Promotion> GetPromotionForDescription(string description);
        public Task<Promotion> GetPromotionById(int id); 
        Task<List<Promotion>> GetAllValidsPromotion();
        public Task CreatePromotion(Promotion promotion);
        public Task DeletePromotion(int id);
        public Task UpdatePromotion(Promotion promotion);

    }
}
