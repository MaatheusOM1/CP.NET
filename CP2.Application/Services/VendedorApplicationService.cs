using CP2.Domain.Entities;
using CP2.Domain.Interfaces;
using CP2.Domain.Interfaces.Dtos;

namespace CP2.Application.Services
{
    public class VendedorApplicationService : IVendedorApplicationService
    {
        private readonly IVendedorRepository _vendedorRepository;

        public VendedorApplicationService(IVendedorRepository vendedorRepository)
        {
            _vendedorRepository = vendedorRepository;
        }

        public VendedorEntity? DeletarDadosVendedor(int id)
        {
            var vendedorRemovido = _vendedorRepository.DeletarDados(id);
            return vendedorRemovido;
        }

        public VendedorEntity? EditarDadosVendedor(int id, IVendedorDto vendedorDto)
        {
            vendedorDto.Validate();

            var vendedorEditado = new VendedorEntity
            {
                Id = id,
                Nome = vendedorDto.Nome,
                Email = vendedorDto.Email,
                Telefone = vendedorDto.Telefone,
                DataNascimento = vendedorDto.DataNascimento,
                Endereco = vendedorDto.Endereco,
                DataContratacao = vendedorDto.DataContratacao,
                ComissaoPercentual = vendedorDto.ComissaoPercentual,
                MetaMensal = vendedorDto.MetaMensal,
                CriadoEm = vendedorDto.CriadoEm
            };

            var vendedorAtualizado = _vendedorRepository.EditarDados(vendedorEditado);
            return vendedorAtualizado;
        }

        public VendedorEntity? ObterVendedorPorId(int id)
        {
            var vendedor = _vendedorRepository.ObterPorId(id);
            return vendedor;
        }

        public IEnumerable<VendedorEntity> ObterTodosVendedores()
        {
            var todosVendedores = _vendedorRepository.ObterTodos();
            return todosVendedores;
        }

        public VendedorEntity? SalvarDadosVendedor(IVendedorDto vendedorDto)
        {
            vendedorDto.Validate();

            var novoVendedor = new VendedorEntity
            {
                Nome = vendedorDto.Nome,
                Email = vendedorDto.Email,
                Telefone = vendedorDto.Telefone,
                DataNascimento = vendedorDto.DataNascimento,
                Endereco = vendedorDto.Endereco,
                DataContratacao = vendedorDto.DataContratacao,
                ComissaoPercentual = vendedorDto.ComissaoPercentual,
                MetaMensal = vendedorDto.MetaMensal,
                CriadoEm = vendedorDto.CriadoEm
            };

            var vendedorSalvo = _vendedorRepository.SalvarDados(novoVendedor);
            return vendedorSalvo;
        }
    }
}
