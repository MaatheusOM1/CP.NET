namespace CP2.Domain.Interfaces.Dtos
{
    public interface IFornecedorDto
    {
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public DateTime CriadoEm { get; set; }
        void Validate();
    }
}
