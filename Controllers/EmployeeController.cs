using GameRentalStore.Controllers.Models;
using GameRentalStore.Controllers.Models.Views;
using GameRentalStore.Models.Validators.Employee;
using GameRentalStore.Models.Views.Employee;
using GameRentalStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameRentalStore.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(ITokenService tokenService, IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("v1/login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            try
            {
                var validator = new LoginRequestValidator();
                var validate = validator.Validate(loginRequest);

                if (!validate.IsValid)
                    return UnprocessableEntity(new ResultViewModel<LoginResponse>(erros: validate.Errors.Select(a => a.ErrorMessage.ToString()).ToList()));

                var token = await _employeeService.Login(loginRequest);

                if (token.Sucess)
                    return Ok(token);

                return Unauthorized(new ResultViewModel<LoginResponse>(token.Errors));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao fazer login, tente novamente.");
            }
        }

        [HttpPost("v1/register")]
        public async Task<IActionResult> Register([FromBody] CreateEmployeeViewModel employee)
        {
            try
            {
                var validator = new CreateEmployeeViewModelValidator();
                var validate = validator.Validate(employee);
                if (!validate.IsValid)
                    return UnprocessableEntity(new ResultViewModel<Employee>(erros: validate.Errors.Select(a => a.ErrorMessage.ToString()).ToList()));

                var result = await _employeeService.Register(employee);
                if (result.Sucess)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao fazer registrar usuário, tente novamente.");
            }
        }
    }
}