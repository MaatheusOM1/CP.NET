using CP2.Domain.Interfaces.Dtos;
using FluentValidation;
using System.Globalization;

namespace CP2.Application.Dtos
{
    public class FornecedorDto : IFornecedorDto
    {
        public string Nome { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CriadoEm { get; set; }

        public void Validate()
        {
            var validator = new FornecedorDtoValidator();
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

    internal class FornecedorDtoValidator : AbstractValidator<FornecedorDto>
    {
        public FornecedorDtoValidator()
        {
            RuleFor(fornecedor => fornecedor.Nome)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Nome é um campo obrigatório e não pode estar vazio.")
                .Must(nome => nome.Length >= 5).WithMessage("O Nome precisa conter pelo menos 5 caracteres.");

            RuleFor(fornecedor => fornecedor.CNPJ)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("CNPJ é obrigatório.")
                .Must(cnpj => !string.IsNullOrWhiteSpace(cnpj)).WithMessage("CNPJ não pode ser apenas espaços em branco.");

            RuleFor(fornecedor => fornecedor.Endereco)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("É necessário fornecer um endereço.")
                .Must(endereco => endereco.Trim().Length > 0).WithMessage("Endereço inválido.");

            RuleFor(fornecedor => fornecedor.Telefone)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Telefone é um campo obrigatório.")
                .Matches(@"^\+?\d+$").WithMessage("O telefone deve conter apenas números e um possível sinal de '+'.");

            RuleFor(fornecedor => fornecedor.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("O campo Email não pode ser deixado vazio.")
                .Must(email => email.Length >= 5).WithMessage("Email precisa ter no mínimo 5 caracteres.")
                .EmailAddress().WithMessage("O formato do email é inválido.");

            RuleFor(fornecedor => fornecedor.CriadoEm)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("A data de criação é obrigatória.")
                .Must(data => data != default).WithMessage("Data de criação inválida.");
        }
    }
}
