using GameRentalStore.Controllers.Models;
namespace GameRentalStore.Services.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(Employee employee);
    }
}