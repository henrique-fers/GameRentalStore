using GameRentalStore.Controllers.Models;

namespace GameRentalStore.Models
{
    public class Devolution
    {
        public int Id { get; set; }
        public int IdGame { get; set; }
        public int IdOrder { get; set; }
        public DateTime Date { get; set; }
        public decimal AdditionalValue { get; set; }
        public Game Game { get; set; }
        public Order Order { get; set; }

    }
}
