using CP2.Application.Dtos;
using CP2.Domain.Entities;
using CP2.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CP2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedorController : ControllerBase
    {
        private readonly IFornecedorApplicationService _applicationService;

        public FornecedorController(IFornecedorApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        /// <summary>
        /// Método para obter todos os dados do Fornecedor
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces(typeof(IEnumerable<FornecedorEntity>))]
        public IActionResult GetAllFornecedores()
        {
            var fornecedores = _applicationService.ObterTodosFornecedores();

            return fornecedores != null
                ? Ok(fornecedores)
                : StatusCode((int)HttpStatusCode.NoContent, "Nenhum fornecedor encontrado");
        }

        /// <summary>
        /// Método para obter um fornecedor específico pelo ID
        /// </summary>
        /// <param name="id">Identificador do fornecedor</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces(typeof(FornecedorEntity))]
        public IActionResult GetFornecedorById(int id)
        {
            var fornecedor = _applicationService.ObterFornecedorPorId(id);

            return fornecedor != null
                ? Ok(fornecedor)
                : NotFound($"Fornecedor com ID {id} não encontrado");
        }

        /// <summary>
        /// Método para salvar um novo fornecedor
        /// </summary>
        /// <param name="entity">Modelo de dados do Fornecedor</param>
        /// <returns></returns>
        [HttpPost]
        [Produces(typeof(FornecedorEntity))]
        public IActionResult CreateFornecedor([FromBody] FornecedorDto entity)
        {
            try
            {
                var novoFornecedor = _applicationService.SalvarDadosFornecedor(entity);

                return novoFornecedor != null
                    ? CreatedAtAction(nameof(GetFornecedorById), new { id = novoFornecedor.Id }, novoFornecedor)
                    : BadRequest("Erro ao salvar fornecedor");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    Message = "Ocorreu um erro durante o processamento",
                    Error = ex.Message
                });
            }
        }

        /// <summary>
        /// Método para editar um fornecedor existente
        /// </summary>
        /// <param name="id">Identificador do fornecedor</param>
        /// <param name="entity">Modelo de dados do Fornecedor</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces(typeof(FornecedorEntity))]
        public IActionResult UpdateFornecedor(int id, [FromBody] FornecedorDto entity)
        {
            try
            {
                var fornecedorAtualizado = _applicationService.EditarDadosFornecedor(id, entity);

                return fornecedorAtualizado != null
                    ? Ok(fornecedorAtualizado)
                    : NotFound($"Fornecedor com ID {id} não encontrado para atualização");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    Message = "Erro ao atualizar fornecedor",
                    Error = ex.Message
                });
            }
        }

        /// <summary>
        /// Método para deletar um fornecedor
        /// </summary>
        /// <param name="id">Identificador do fornecedor</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces(typeof(FornecedorEntity))]
        public IActionResult RemoveFornecedor(int id)
        {
            var fornecedorDeletado = _applicationService.DeletarDadosFornecedor(id);

            return fornecedorDeletado != null
                ? Ok(fornecedorDeletado)
                : NotFound($"Fornecedor com ID {id} não encontrado para exclusão");
        }
    }
}
