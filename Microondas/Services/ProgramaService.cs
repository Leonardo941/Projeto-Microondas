using Microondas.Context;
using Microondas.Models;
using System.Linq;

namespace Microondas.Services
{
    public class ProgramaService
    {
        private readonly ProgramaContext _context;
        private static readonly string CaracteresPermitidos = "'!@#$%¨&*()_-+=´`[{~^}],;/?";

        public ProgramaService(ProgramaContext context)
        {
            _context = context;
        }

        public bool ValidaCaractere(string caractere)
        {
            var caracteresEmUso = _context.Programas
                .Select(p => p.Caractere)
                .Where(c => c != null)
                .ToList();

            return !caracteresEmUso.Contains(caractere);
        }

        public string? ObterCaractereDisponivel()
        {
            var caracteresEmUso = _context.Programas
                .Select(p => p.Caractere)
                .Where(c => c != null)
                .ToList();

            var caracteresDisponiveis = CaracteresPermitidos
                .Where(c => !caracteresEmUso.Contains(c.ToString()))
                .ToList();

            if (!caracteresDisponiveis.Any())
            {
                return null;
            }

            var random = new Random();
            return caracteresDisponiveis[random.Next(caracteresDisponiveis.Count)].ToString();
        }
    }
}