using FluentValidation;
using GameRentalStore.Models.Views.Employee;

namespace GameRentalStore.Models.Validators.Employee
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator() {
            RuleFor(x => x.Email).NotEmpty().WithMessage("O Email é obrigatório.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("A Senha é obrigatório.");
        } 
    }
}
