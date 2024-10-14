using CP2.Domain.Interfaces.Dtos;
using FluentValidation;
using System.Globalization;

namespace CP2.Application.Dtos
{
    public class VendedorDto : IVendedorDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Endereco { get; set; } = string.Empty;
        public DateTime DataContratacao { get; set; }
        public decimal ComissaoPercentual { get; set; }
        public decimal MetaMensal { get; set; }
        public DateTime CriadoEm { get; set; }

        public void Validate()
        {
            var validator = new VendedorDtoValidator();
            var validationResult = validator.Validate(this);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors
                    .Select(error => error.ErrorMessage)
                    .Aggregate((message1, message2) => $"{message1} e {message2}");
                throw new Exception(errorMessages);
            }
        }
    }

    internal class VendedorDtoValidator : AbstractValidator<VendedorDto>
    {
        public VendedorDtoValidator()
        {
            RuleFor(vendedor => vendedor.Nome)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Nome é um campo obrigatório e não pode estar vazio.")
                .Must(nome => nome.Length >= 5).WithMessage("O Nome precisa conter pelo menos 5 caracteres.");

            RuleFor(vendedor => vendedor.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("O campo Email não pode ser deixado vazio.")
                .Must(email => email.Length >= 5).WithMessage("Email precisa ter no mínimo 5 caracteres.")
                .EmailAddress().WithMessage("O formato do email é inválido.");

            RuleFor(vendedor => vendedor.Telefone)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Telefone é um campo obrigatório.")
                .Matches(@"^\+?\d+$").WithMessage("O telefone deve conter apenas números e um possível sinal de '+'.");

            RuleFor(vendedor => vendedor.DataNascimento)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("A data de nascimento é obrigatória.")
                .Must(data => data != default).WithMessage("Data de nascimento inválida.");

            RuleFor(vendedor => vendedor.Endereco)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("É necessário fornecer um endereço.")
                .Must(endereco => endereco.Trim().Length > 0).WithMessage("Endereço inválido.");

            RuleFor(vendedor => vendedor.DataContratacao)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("A data de contratação é obrigatória.")
                .Must(data => data != default).WithMessage("Data de contratação inválida.");

            RuleFor(vendedor => vendedor.ComissaoPercentual)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThan(0).WithMessage("A comissão percentual deve ser maior que zero.");

            RuleFor(vendedor => vendedor.MetaMensal)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThan(0).WithMessage("A meta mensal deve ser maior que zero.");

            RuleFor(vendedor => vendedor.CriadoEm)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("A data de criação é obrigatória.")
                .Must(data => data != default).WithMessage("Data de criação inválida.");
        }
    }
}
