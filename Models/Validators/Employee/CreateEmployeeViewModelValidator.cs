using FluentValidation;
using GameRentalStore.Controllers.Models;
using GameRentalStore.Models.Views.Employee;

namespace GameRentalStore.Models.Validators.Employee
{
    public class CreateEmployeeViewModelValidator : AbstractValidator<CreateEmployeeViewModel>
    {
        public CreateEmployeeViewModelValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("O nome é obrigatório");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("O sobrenome é obrigatório");

            RuleFor(x => x.Email).NotEmpty().WithMessage("O email é obrigatório.").EmailAddress();

            RuleFor(x => x.Address).Length(20, 250).WithMessage("O endereço deve conter entre 20 e 250 caracteres");

            RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("A senha é obrigatório");

            RuleFor(x => x.Birthday).NotEmpty().WithMessage("informe a data de nascimento.");
            
            //Ajustar depois 
            //RuleFor(x => x.PhoneNumber)
            //.NotEmpty().WithMessage("O número de telefone é obrigatório.")
            // .Matches(@"^\(?\d{2}\)?[\s-]?\d{4,5}-?\d{4}$").WithMessage("Por favor, insira um número de telefone válido no formato (XX) XXXX-XXXX ou (XX) XXXXX-XXXX.");
        }
    }
}
