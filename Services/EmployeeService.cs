using FluentValidation;
using GameRentalStore.Controllers.Models;
using GameRentalStore.Controllers.Models.Views;
using GameRentalStore.Models.Views.Employee;
using GameRentalStore.Repositories.Interfaces;
using GameRentalStore.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using SecureIdentity.Password;
using System.Data;

namespace GameRentalStore.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITokenService _tokenService;
        public EmployeeService(IEmployeeRepository employeeRepository, ITokenService tokenService)
        {
            _employeeRepository = employeeRepository;
            _tokenService = tokenService;
        }

        public async Task<ResultViewModel<LoginResponse>> Login(LoginRequest login)
        {
            var user = await _employeeRepository.GetByEmail(login.Email);

            if (user == null)
                new ResultViewModel<LoginResponse>("Email ou senha incorretos.");

            if (!PasswordHasher.Verify(user.Password, login.Password ))
                new ResultViewModel<LoginResponse>("Email ou senha incorretos.");

            var token = _tokenService.GenerateToken(user);
            return new ResultViewModel<LoginResponse>(new LoginResponse { Token = token});
        }

        public async Task<ResultViewModel<Employee>> Register(CreateEmployeeViewModel employeeToCreate)
        {
            var employee = await _employeeRepository.GetByEmail(employeeToCreate.Email);

            if (employee != null)
                return new ResultViewModel<Employee>("Email já cadastrado, por favor informar um novo ou resgatar a senha.");

            employee = MapRegisterEmployee(employeeToCreate);

            employee.Password = EncryptPassword(employeeToCreate.Password);

            await _employeeRepository.CreateEmployee(employee);

            employee.Password = "";

            return new ResultViewModel<Employee>(employee);
        }

        private string EncryptPassword(string password)
        {
            var passwordHashed = PasswordHasher.Hash(password,saltSize:16);
            return passwordHashed;
        }

        private Employee MapRegisterEmployee(CreateEmployeeViewModel createEmployee)
            => new Employee
            {

                Address = createEmployee.Address,
                Birthday = createEmployee.Birthday,
                Email = createEmployee.Email,
                FirstName = createEmployee.FirstName,
                LastName = createEmployee.LastName,
                Password = createEmployee.Password,
                PhoneNumber = createEmployee.PhoneNumber,
                Role = createEmployee.Role
            };

    }
}
