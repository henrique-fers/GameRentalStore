namespace GameRentalStore.Controllers.Models
{
    public class GameAvailable
    {
        public int Id { get; set; }
        public decimal Rate { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public int IdGame { get; set; }
        public int IdCustomer { get; set; }
        public Game Game { get; set; }
        public Customer Customer { get; set; }
    }
}