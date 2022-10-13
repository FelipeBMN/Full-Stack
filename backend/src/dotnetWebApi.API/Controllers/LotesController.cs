using Microsoft.AspNetCore.Mvc;
using dotnetWebApi.Application.Contratos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using dotnetWebApi.Application.Dtos;

namespace dotnetWebApi.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LotesController : ControllerBase
    {

        private readonly ILoteService loteService;
 
        public LotesController(ILoteService loteService)
        {
            this.loteService = loteService;
        }

        [HttpGet("{eventoId}")]
        public async Task<IActionResult> GetAllLotesByEventIdAsync(int eventoId) // IActionResult Permite retornar o status code
        {
            try
            {
                var eventos = await this.loteService.GetAllLotesByEventIdAsync(eventoId);
                if (eventos == null) return NotFound("Nenhum evento encontrado");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpPut("{eventoId}")]
        public async Task<IActionResult> SaveLotes(int eventoId, LoteDto[] models)
        {
            try
            {
                var lotes = await this.loteService.SaveLotes(eventoId, models);
                if (lotes == null) return NoContent(); 

                return Ok(lotes);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar salvar lotes. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{eventoId}/{loteId}")]
        public async Task<IActionResult> Delete(int eventoId, int loteId)
        {
            try
            {
                var lote = await loteService.GetLoteByIdAsync(eventoId, loteId);
                if(lote == null) return NoContent();

                return await this.loteService.DeleteLote(eventoId, loteId) 
                ? Ok(new{ message = "Deletado"}) 
                : BadRequest("Não Deletado");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar lote. Erro: {ex.Message}");
            }
        }
    }
}
