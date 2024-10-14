using CP2.Application.Dtos;
using CP2.Domain.Entities;
using CP2.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CP2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendedorController : ControllerBase
    {
        private readonly IVendedorApplicationService _applicationService;

        public VendedorController(IVendedorApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        /// <summary>
        /// Método para obter todos os vendedores
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces(typeof(IEnumerable<VendedorEntity>))]
        public IActionResult GetAllVendedores()
        {
            var vendedores = _applicationService.ObterTodosVendedores();

            return vendedores != null
                ? Ok(vendedores)
                : StatusCode((int)HttpStatusCode.NoContent, "Nenhum vendedor encontrado");
        }

        /// <summary>
        /// Método para obter um vendedor pelo ID
        /// </summary>
        /// <param name="id">Identificador do vendedor</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces(typeof(VendedorEntity))]
        public IActionResult GetVendedorById(int id)
        {
            var vendedor = _applicationService.ObterVendedorPorId(id);

            return vendedor != null
                ? Ok(vendedor)
                : NotFound($"Vendedor com ID {id} não encontrado");
        }

        /// <summary>
        /// Método para criar um novo vendedor
        /// </summary>
        /// <param name="entity">Modelo de dados do Vendedor</param>
        /// <returns></returns>
        [HttpPost]
        [Produces(typeof(VendedorEntity))]
        public IActionResult CreateVendedor([FromBody] VendedorDto entity)
        {
            try
            {
                var novoVendedor = _applicationService.SalvarDadosVendedor(entity);

                return novoVendedor != null
                    ? CreatedAtAction(nameof(GetVendedorById), new { id = novoVendedor.Id }, novoVendedor)
                    : BadRequest("Erro ao salvar vendedor");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    Message = "Ocorreu um erro ao processar sua solicitação",
                    Error = ex.Message
                });
            }
        }

        /// <summary>
        /// Método para editar os dados de um vendedor existente
        /// </summary>
        /// <param name="id">Identificador do vendedor</param>
        /// <param name="entity">Modelo de dados do Vendedor</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces(typeof(VendedorEntity))]
        public IActionResult UpdateVendedor(int id, [FromBody] VendedorDto entity)
        {
            try
            {
                var vendedorAtualizado = _applicationService.EditarDadosVendedor(id, entity);

                return vendedorAtualizado != null
                    ? Ok(vendedorAtualizado)
                    : NotFound($"Vendedor com ID {id} não encontrado para atualização");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    Message = "Erro ao atualizar vendedor",
                    Error = ex.Message
                });
            }
        }

        /// <summary>
        /// Método para deletar um vendedor pelo ID
        /// </summary>
        /// <param name="id">Identificador do vendedor</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces(typeof(VendedorEntity))]
        public IActionResult RemoveVendedor(int id)
        {
            var vendedorDeletado = _applicationService.DeletarDadosVendedor(id);

            return vendedorDeletado != null
                ? Ok(vendedorDeletado)
                : NotFound($"Vendedor com ID {id} não encontrado para exclusão");
        }
    }
}
