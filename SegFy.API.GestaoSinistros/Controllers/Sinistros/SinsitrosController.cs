using Microsoft.AspNetCore.Mvc;
using SegFy.API.GestaoSinistros.Application.DTO_s.Requests;
using SegFy.API.GestaoSinistros.Application.Exceptions;
using SegFy.API.GestaoSinistros.Application.Interfaces;

namespace SegFy.API.GestaoSinistros.Controllers.Sinistros
{
    [ApiController]
    [Route("api/sinistros")]
    public class SinsitrosController : Controller
    {
        private readonly ISinistroService _sinistroService;
        private readonly IHistoricoService _historicoService;
        public SinsitrosController(ISinistroService sinistroService, IHistoricoService historicoService)
        {
            _sinistroService = sinistroService;
            _historicoService = historicoService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(AbrirSinistroRequest request)
        {
            try
            {
                await _sinistroService.AbrirSinistroAsync(request);
                return Created();
            }
            catch (NotFoundException e)
            {
                return BadRequest(e.Message);
            }
            catch (BusinessException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscaSinistroPorId(int id)
        {

            var sinistro = await _sinistroService.BuscarSinistroPorIdAsync(id);

            if (sinistro == null)
                return NotFound();

            return Ok(sinistro);
        }
        [HttpGet]
        public async Task<IActionResult> BuscaSinistroPorFiltro([FromQuery]SinistroFiltroRequest request)
        {
            var sinistros = await _sinistroService.BuscaSinistrosPorfiltroAsync(request);

            if (sinistros == null)
                return NotFound();

            return Ok(sinistros);
        }

        [HttpGet("{id}/historico")]
        public async Task<IActionResult> BuscaHistoricoPorId(int id)
        {
            var historico = await _historicoService.ConsultarHistoricoAsync(id);

            if (historico == null)
                return NotFound();

            return Ok(historico);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> Patch(int id,[FromQuery]AtualizarStatusSinistroRequest request) 
        {
            try
            {
                await _sinistroService.AtualizarStatusSinistroAsync(id,request);
                return NoContent();
            }
            catch (NotFoundException e)
            {
                return BadRequest(e.Message);
            }
            catch(BusinessException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
