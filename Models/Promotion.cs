using GameRentalStore.Models.Enums;

namespace GameRentalStore.Controllers.Models
{

    public class Promotion
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }
        public DateTime Validation { get; set; }
        public PromotionType PromotionType { get; set; }
    }
}