using GameRentalStore.Controllers.Models;

namespace GameRentalStore.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetByEmail(string email);
        Task CreateEmployee(Employee employee);
    }
}
