using CP2.Domain.Entities;
using CP2.Domain.Interfaces;
using CP2.Domain.Interfaces.Dtos;

namespace CP2.Application.Services
{
    public class FornecedorApplicationService : IFornecedorApplicationService
    {
        private readonly IFornecedorRepository _fornecedorRepository;

        public FornecedorApplicationService(IFornecedorRepository fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;
        }

        public FornecedorEntity? DeletarDadosFornecedor(int id)
        {
            var fornecedorRemovido = _fornecedorRepository.DeletarDados(id);
            return fornecedorRemovido;
        }

        public FornecedorEntity? EditarDadosFornecedor(int id, IFornecedorDto fornecedorDto)
        {
            fornecedorDto.Validate();

            var fornecedor = new FornecedorEntity
            {
                Id = id,
                Nome = fornecedorDto.Nome,
                CNPJ = fornecedorDto.CNPJ,
                Endereco = fornecedorDto.Endereco,
                Telefone = fornecedorDto.Telefone,
                Email = fornecedorDto.Email,
                CriadoEm = fornecedorDto.CriadoEm
            };

            var fornecedorAtualizado = _fornecedorRepository.EditarDados(fornecedor);
            return fornecedorAtualizado;
        }

        public FornecedorEntity? ObterFornecedorPorId(int id)
        {
            var fornecedor = _fornecedorRepository.ObterPorId(id);
            return fornecedor;
        }

        public IEnumerable<FornecedorEntity> ObterTodosFornecedores()
        {
            var fornecedores = _fornecedorRepository.ObterTodos();
            return fornecedores;
        }

        public FornecedorEntity? SalvarDadosFornecedor(IFornecedorDto fornecedorDto)
        {
            fornecedorDto.Validate();

            var novoFornecedor = new FornecedorEntity
            {
                Nome = fornecedorDto.Nome,
                CNPJ = fornecedorDto.CNPJ,
                Endereco = fornecedorDto.Endereco,
                Telefone = fornecedorDto.Telefone,
                Email = fornecedorDto.Email,
                CriadoEm = fornecedorDto.CriadoEm
            };

            var fornecedorSalvo = _fornecedorRepository.SalvarDados(novoFornecedor);
            return fornecedorSalvo;
        }
    }
}
