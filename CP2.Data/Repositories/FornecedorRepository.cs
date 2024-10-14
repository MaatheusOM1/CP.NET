using CP2.Data.AppData;
using CP2.Domain.Entities;
using CP2.Domain.Interfaces;
using System.Linq;

namespace CP2.Data.Repositories
{
    public class FornecedorRepository : IFornecedorRepository
    {
        private readonly ApplicationContext _dbContext;

        public FornecedorRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public FornecedorEntity? RemoveFornecedor(int id)
        {
            var fornecedor = _dbContext.Fornecedor.SingleOrDefault(f => f.Id == id);

            if (fornecedor != null)
            {
                _dbContext.Fornecedor.Remove(fornecedor);
                _dbContext.SaveChanges();
                return fornecedor;
            }

            return null;
        }

        public FornecedorEntity? UpdateFornecedor(FornecedorEntity updatedEntity)
        {
            var existingFornecedor = _dbContext.Fornecedor.SingleOrDefault(f => f.Id == updatedEntity.Id);

            if (existingFornecedor != null)
            {
                existingFornecedor.Nome = updatedEntity.Nome;
                existingFornecedor.CNPJ = updatedEntity.CNPJ;
                existingFornecedor.Endereco = updatedEntity.Endereco;
                existingFornecedor.Telefone = updatedEntity.Telefone;
                existingFornecedor.Email = updatedEntity.Email;
                existingFornecedor.CriadoEm = updatedEntity.CriadoEm;

                _dbContext.SaveChanges();
                return existingFornecedor;
            }

            return null;
        }

        public FornecedorEntity? GetFornecedorById(int id)
        {
            return _dbContext.Fornecedor.SingleOrDefault(f => f.Id == id);
        }

        public IEnumerable<FornecedorEntity> GetAllFornecedores()
        {
            return _dbContext.Fornecedor.ToList();
        }

        public FornecedorEntity? AddFornecedor(FornecedorEntity newEntity)
        {
            _dbContext.Fornecedor.Add(newEntity);
            _dbContext.SaveChanges();
            return newEntity;
        }

        IEnumerable<FornecedorEntity> IFornecedorRepository.ObterTodos()
        {
            throw new NotImplementedException();
        }

        FornecedorEntity? IFornecedorRepository.ObterPorId(int id)
        {
            throw new NotImplementedException();
        }

        FornecedorEntity? IFornecedorRepository.SalvarDados(FornecedorEntity entity)
        {
            throw new NotImplementedException();
        }

        FornecedorEntity? IFornecedorRepository.EditarDados(FornecedorEntity entity)
        {
            throw new NotImplementedException();
        }

        FornecedorEntity? IFornecedorRepository.DeletarDados(int id)
        {
            throw new NotImplementedException();
        }
    }
}
