using CP2.Data.AppData;
using CP2.Domain.Entities;
using CP2.Domain.Interfaces;
using System.Linq;

namespace CP2.Data.Repositories
{
    public class VendedorRepository : IVendedorRepository
    {
        private readonly ApplicationContext _dbContext;

        public VendedorRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public VendedorEntity? RemoveVendedor(int id)
        {
            var vendedor = _dbContext.Vendedor.SingleOrDefault(v => v.Id == id);

            if (vendedor != null)
            {
                _dbContext.Vendedor.Remove(vendedor);
                _dbContext.SaveChanges();
                return vendedor;
            }

            return null;
        }

        public VendedorEntity? UpdateVendedor(VendedorEntity updatedEntity)
        {
            var existingVendedor = _dbContext.Vendedor.SingleOrDefault(v => v.Id == updatedEntity.Id);

            if (existingVendedor != null)
            {
                existingVendedor.Nome = updatedEntity.Nome;
                existingVendedor.Email = updatedEntity.Email;
                existingVendedor.Telefone = updatedEntity.Telefone;
                existingVendedor.DataNascimento = updatedEntity.DataNascimento;
                existingVendedor.Endereco = updatedEntity.Endereco;
                existingVendedor.DataContratacao = updatedEntity.DataContratacao;
                existingVendedor.ComissaoPercentual = updatedEntity.ComissaoPercentual;
                existingVendedor.MetaMensal = updatedEntity.MetaMensal;
                existingVendedor.CriadoEm = updatedEntity.CriadoEm;

                _dbContext.SaveChanges();
                return existingVendedor;
            }

            return null;
        }

        public VendedorEntity? GetVendedorById(int id)
        {
            return _dbContext.Vendedor.SingleOrDefault(v => v.Id == id);
        }

        public IEnumerable<VendedorEntity> GetAllVendedores()
        {
            return _dbContext.Vendedor.ToList();
        }

        public VendedorEntity? AddVendedor(VendedorEntity newEntity)
        {
            _dbContext.Vendedor.Add(newEntity);
            _dbContext.SaveChanges();
            return newEntity;
        }

        IEnumerable<VendedorEntity> IVendedorRepository.ObterTodos()
        {
            throw new NotImplementedException();
        }

        VendedorEntity? IVendedorRepository.ObterPorId(int id)
        {
            throw new NotImplementedException();
        }

        VendedorEntity? IVendedorRepository.SalvarDados(VendedorEntity entity)
        {
            throw new NotImplementedException();
        }

        VendedorEntity? IVendedorRepository.EditarDados(VendedorEntity entity)
        {
            throw new NotImplementedException();
        }

        VendedorEntity? IVendedorRepository.DeletarDados(int id)
        {
            throw new NotImplementedException();
        }
    }
}
