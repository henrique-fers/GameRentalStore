using GameRentalStore.Controllers.Models;

namespace GameRentalStore.Models
{
    public class OrderItem
    { 
        public int Id { get; set; } 
        public int IdOrder { get; set; } 
        public int IdGame { get; set; }
        public decimal Price { get; set; }
        public Order Order { get; set; }
        public Game Game { get; set; }
    }
}
