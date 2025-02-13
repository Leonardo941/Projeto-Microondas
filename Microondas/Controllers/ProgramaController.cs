using Microondas.Context;
using Microondas.Models;
using Microondas.Services;
using Microsoft.AspNetCore.Mvc;

namespace Microondas.Controllers
{
    [ApiController]
    [Route("api/programa")]
    public class ProgramaController : ControllerBase
    {

        private readonly ProgramaContext _context;
        private readonly ProgramaService _programaService;

        public ProgramaController(ProgramaContext context)
        {
            _context = context;
            _programaService = new ProgramaService(context);
        }

        [HttpPost]
        public IActionResult Create(Programa programa)
        {
            if (string.IsNullOrEmpty(programa.Caractere) || !_programaService.ValidaCaractere(programa.Caractere))
            {
                return BadRequest("Caractere já está em uso ou é inválido.");
            }

            programa.Customizado = true;

            _context.Add(programa);
            _context.SaveChanges();
            return Ok(programa);
        }

        [HttpGet]
        public IActionResult ObterTodos()
        {
            var programas = _context.Programas.ToList();

            if (!programas.Any())
            {
                return NotFound();
            }

            return Ok(programas);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var programa = _context.Programas.Find(id);

            if (programa == null)
            {
                return NotFound();
            }

            return Ok(programa);
        }

        /*[HttpPut("{id}")]
        public IActionResult Atualizar(int id, Programa programa)
        {
            var programaBanco = _context.Programas.Find(id);

            if (programaBanco == null)
            {
                return NotFound();
            }

            programaBanco.Potencia = programa.Potencia;
            programaBanco.Alimento = programa.Alimento;
            programaBanco.Instrucao = programa.Instrucao;
            programaBanco.Tempo = programa.Tempo;
            programaBanco.Nome = programa.Nome;

            _context.Programas.Update(programaBanco);
            _context.SaveChanges();

            return Ok(programaBanco);
        }*/

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var programaBanco = _context.Programas.Find(id);

            if (programaBanco == null)
            {
                return NotFound();
            }

            _context.Programas.Remove(programaBanco);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
