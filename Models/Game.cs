using GameRentalStore.Models.Enums;

namespace GameRentalStore.Controllers.Models
{
    public class Game
    {
        public Game()
        {
            GameAvailables = new List<GameAvailable>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Review { get; set; }
        //MELHORAR ESSE NOME ABAIXO AgeIndicated
        public int AgeIndicate { get; set; }
        // Criar uma lista de avaliações substituir o rate.    
        public decimal MediaRate { get; set; }
        public Gender Gender { get; set; }
        public List<GameAvailable> GameAvailables { get; set; }
    }
}