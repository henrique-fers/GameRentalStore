using GameRentalStore.Controllers.Models;
using GameRentalStore.Controllers.Models.Views;
using GameRentalStore.Models.Views.Employee;

namespace GameRentalStore.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<ResultViewModel<Employee>> Register(CreateEmployeeViewModel employeeToCreate);
        Task<ResultViewModel<LoginResponse>> Login(LoginRequest login);
    }
}
