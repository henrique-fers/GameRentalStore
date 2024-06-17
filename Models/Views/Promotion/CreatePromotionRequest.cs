using GameRentalStore.Models.Enums;

namespace GameRentalStore.Models.Views.Promotion
{
    public class CreatePromotionRequest
    {
        public string Code {  get; set; }
        public int Value {  get; set; }
        public string Description { get; set; }
        public DateTime Validation { get; set; }
        public PromotionType PromotionType { get; set; }
    }
}
