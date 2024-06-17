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
                return new ResultViewModel<Promotion>("N�o foi poss�vel encontrar uma promo��o com esse Id!");

            var promotion = await _promotionRepository.GetPromotionById(id);

            if (promotion == null)
                return new ResultViewModel<Promotion>("promo��o n�o encontrada!");

            return new ResultViewModel<Promotion>(promotion);
        }


        public async Task<ResultViewModel<Promotion>> GetPromotionByDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
                return new ResultViewModel<Promotion>("Por favor informe a descri��o da promo��o");


            var promotion = await _promotionRepository.GetPromotionForDescription(description);

            if (promotion == null)
                return new ResultViewModel<Promotion>("Promo��o n�o encontrada");

            return new ResultViewModel<Promotion>(promotion);
        }

        public async Task<ResultViewModel<List<Promotion>>> GetAllPromotion()
        {
            var promotion = await _promotionRepository.GetAllPromotion();
            if (promotion == null)
                return new ResultViewModel<List<Promotion>>("nenhuma promo��o foi encontrada");

            return new ResultViewModel<List<Promotion>>(promotion);
        }

        public async Task<ResultViewModel<List<Promotion>>> GetAllValidsPromotion()
        {
            var promotion = await _promotionRepository.GetAllValidsPromotion();
            if (promotion == null)
                return new ResultViewModel<List<Promotion>>("nenhuma promo��o foi encontrada");

            return new ResultViewModel<List<Promotion>>(promotion);
        }

        public async Task<ResultViewModel<Promotion>> Create(CreatePromotionRequest promotion)
        {
            try
            {
                if (promotion.Validation < DateTime.Now)
                    return new ResultViewModel<Promotion>("N�o � poss�vel adicionar a promo��o, pois a data valida��o � menor que a data atual.");

                var promotionExists = GetPromotionByDescription(promotion.Description);
                if (promotionExists != null)
                    return new ResultViewModel<Promotion>("N�o � poss�vel adicionar a promo��o, pois j� existe uma com a mesma descri��o.");

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

                return new ResultViewModel<Promotion>("Erro ao inserir promo��o");
            }
        }

        public Task CreatePromotion(Promotion promotion)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultViewModel<Promotion>> DeletePromotion(int id)
        {
            if (id == 0 || id < 0)
                return new ResultViewModel<Promotion>("N�o foi poss�vel deletar uma promo��o com esse Id!");

            var promotionToDelete = await GetPromotionById(id);

            if (promotionToDelete.Data == null)
                return new ResultViewModel<Promotion>("N�o foi encontrada uma promo��o com esse Id para deletar!");
            
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
