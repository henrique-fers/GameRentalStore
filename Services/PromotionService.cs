using GameRentalStore.Controllers.Models;
using GameRentalStore.Controllers.Models.Views;
using GameRentalStore.Models.Views.Promotion;
using GameRentalStore.Repositories.Interfaces;
using GameRentalStore.Services.Interfaces;

namespace GameRentalStore.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepository _promotionRepository;

        public PromotionService(IPromotionRepository promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }

        public async Task<ResultViewModel<Promotion>> GetPromotionById(int id)
        {
            if (id == 0 || id < 0)
                return new ResultViewModel<Promotion>("Não foi possível encontrar uma promoção com esse Id!");

            var promotion = await _promotionRepository.GetPromotionById(id);

            if (promotion == null)
                return new ResultViewModel<Promotion>("promoção não encontrada!");

            return new ResultViewModel<Promotion>(promotion);
        }


        public async Task<ResultViewModel<Promotion>> GetPromotionByDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
                return new ResultViewModel<Promotion>("Por favor informe a descrição da promoção");


            var promotion = await _promotionRepository.GetPromotionForDescription(description);

            if (promotion == null)
                return new ResultViewModel<Promotion>("Promoção não encontrada");

            return new ResultViewModel<Promotion>(promotion);
        }

        public async Task<ResultViewModel<List<Promotion>>> GetAllPromotion()
        {
            var promotion = await _promotionRepository.GetAllPromotion();
            if (promotion == null)
                return new ResultViewModel<List<Promotion>>("nenhuma promoção foi encontrada");

            return new ResultViewModel<List<Promotion>>(promotion);
        }

        public async Task<ResultViewModel<List<Promotion>>> GetAllValidsPromotion()
        {
            var promotion = await _promotionRepository.GetAllValidsPromotion();
            if (promotion == null)
                return new ResultViewModel<List<Promotion>>("nenhuma promoção foi encontrada");

            return new ResultViewModel<List<Promotion>>(promotion);
        }

        public async Task<ResultViewModel<Promotion>> Create(CreatePromotionRequest promotion)
        {
            try
            {
                if (promotion.Validation < DateTime.Now)
                    return new ResultViewModel<Promotion>("Não é possível adicionar a promoção, pois a data validação é menor que a data atual.");

                var promotionExists = GetPromotionByDescription(promotion.Description);
                if (promotionExists != null)
                    return new ResultViewModel<Promotion>("Não é possível adicionar a promoção, pois já existe uma com a mesma descrição.");

                await _promotionRepository.CreatePromotion(new Promotion
                {
                    Code = promotion.Code,
                    Value = promotion.Value,
                    Description = promotion.Description,
                    PromotionType = promotion.PromotionType,
                    Validation = promotion.Validation
                });

                return new ResultViewModel<Promotion>(new Promotion());

            }
            catch (Exception ex)
            {

                return new ResultViewModel<Promotion>("Erro ao inserir promoção");
            }
        }

        public Task CreatePromotion(Promotion promotion)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultViewModel<Promotion>> DeletePromotion(int id)
        {
            if (id == 0 || id < 0)
                return new ResultViewModel<Promotion>("Não foi possível deletar uma promoção com esse Id!");

            var promotionToDelete = await GetPromotionById(id);

            if (promotionToDelete.Data == null)
                return new ResultViewModel<Promotion>("Não foi encontrada uma promoção com esse Id para deletar!");
            
            if (promotionToDelete.Sucess == false)
                 
            await _promotionRepository.DeletePromotion(promotionToDelete.Data.Id);

            return new ResultViewModel<Promotion>(new Promotion());
        }

        public Task UpdatePromotion(Promotion promotion)
        {
            throw new NotImplementedException();
        }
    }
}
