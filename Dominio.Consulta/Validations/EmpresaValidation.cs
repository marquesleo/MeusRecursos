using Domain.Consulta.Entities;
using FluentValidation;
namespace Domain.Consulta.Validations
{
    public class EmpresaValidation : AbstractValidator<Empresa>
    {
        public EmpresaValidation()
        {
            RuleFor(c => c.Nome)
                 .NotEmpty().WithMessage("O campo {PropertyName} precisa ser informado")
                 .Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.cnpj)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser informado")
                .Length(18).WithMessage("O campo {PropertyName} precisa ter {MaxLength} caracteres");

            RuleFor(c => c.Telefone)
              .NotEmpty().WithMessage("O campo {PropertyName} precisa ser informado")
              .Length(14).WithMessage("O campo {PropertyName} deve ter {MaxLength} caracteres");


        }
    }
}
