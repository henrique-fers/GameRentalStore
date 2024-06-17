using GameRentalStore.Controllers.Models;

namespace GameRentalStore.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public int IdGame { get; set; }
        public int Quantity { get; set; }
        
        public Game Game { get; set; }
    }
}
