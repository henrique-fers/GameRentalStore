using GameRentalStore.Models.Views.Promotion;
using GameRentalStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameRentalStore.Controllers
{
    [ApiController]
    [Route("api/promotion")]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _promotionService;
        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        [HttpGet]
        [Route("v1/{id:int}")]
        public async Task<IActionResult> GetPromotionById([FromRoute] int id)
        {
            try
            {
                var teste = DateTime.Now;
                var promotion = await _promotionService.GetPromotionById(id);
                if (!promotion.Sucess)
                    return BadRequest(promotion);

                return Ok(promotion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao buscar promoção.");
            }
            
        }

        [HttpGet]
        [Route("v1/{description}")]
        public async Task<IActionResult> GetPromotionByDescription([FromRoute] string description)
        {
            try
            {
                var promotion = await _promotionService.GetPromotionByDescription(description);

                if (!promotion.Sucess)
                    return BadRequest(promotion);

                return Ok(promotion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao buscar promoção.");
            }

        }

        [HttpGet]
        [Route("v1/")]
        public async Task<IActionResult> GetAllPromotion()
        {
            try
            {
                var promotions = await _promotionService.GetAllPromotion();

                if (!promotions.Sucess)
                    return BadRequest(promotions);

                return Ok(promotions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao buscar promoção.");
            }
        }

        [HttpGet]
        [Route("v1/Valid")]
        public async Task<IActionResult> GetAllValidPromotion()
        {
            try
            {
                var promotion = await _promotionService.GetAllValidsPromotion();

                if (!promotion.Sucess)
                    return BadRequest(promotion);

                return Ok(promotion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao buscar promoção.");
            }
        }

        [HttpPost]
        [Route("")]
        public IActionResult Create(CreatePromotionRequest promotion)
        {
            try
            {

                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao criar promoção.");
            }
        }

        [HttpDelete]
        [Route("v1/{id:int}")] 
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var promotionDeleted = await _promotionService.DeletePromotion(id);
                if (!promotionDeleted.Sucess)
                    return BadRequest(promotionDeleted);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao criar promoção.");
            }
        } 
    }
}
