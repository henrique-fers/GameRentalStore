using GameRentalStore.Controllers.Models.Views;
using GameRentalStore.Models.Enums;

namespace GameRentalStore.Controllers.Models
{
    public class Employee : PersonBaseViewModel
    {   
        public Role Role { get; set; }
        public string Password { get; set; }
    }
}