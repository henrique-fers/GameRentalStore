using GameRentalStore.Models.Enums;

namespace GameRentalStore.Controllers.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int QuantityItems { get; set; }
        public decimal Price { get; set; }
        //TRANSFORMAR FORMA DE PAGAMENTO EM ENUM
        public PaymentMethod PaymentMethod { get; set; }
    }
}
