using Microsoft.AspNetCore.Mvc;
using Microondas.Models;
using Microondas.Services;

namespace Microondas.Controllers
{
    [ApiController]
    [Route("api/aquecimento")]
    public class MicroondasController : ControllerBase
    {
        private static MicroondasService _microondas = new MicroondasService();

        [HttpPost("aquecer")]
        public IActionResult Aquecer([FromBody] MicroondasApi microondas)
        {
            try
            {
                _microondas.Aquecer(microondas.Tempo, microondas.Potencia);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }

        [HttpPost("pausar")]
        public IActionResult PausarAquecimento()
        {
            _microondas.Pausar();
            return Ok("Aquecimento pausado.");
        }

        [HttpPost("cancelar")]
        public IActionResult CancelarAquecimento()
        {
            _microondas.Cancelar();
            return Ok("Aquecimento cancelado.");
        }

        [HttpGet("status")]
        public IActionResult ObterStatus()
        {
            return Ok(_microondas.ObterStatus());
        }
    }
}
