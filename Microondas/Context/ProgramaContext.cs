using Microondas.Models;
using Microondas.Services;
using Microsoft.EntityFrameworkCore;

namespace Microondas.Context
{
    public class ProgramaContext : DbContext
    {
        private readonly ProgramaService _programaService;
        public ProgramaContext(DbContextOptions<ProgramaContext> options) : base(options) 
        {
            _programaService = new ProgramaService(this);
        }

        public DbSet<Programa> Programas { get; set; }

        public void SeedProgramas()
        {
            if (!Programas.Any())
            {
                var programasPreDefinidos = new List<Programa>
                {
                    new Programa { Nome = "Pipoca", Alimento = "Pipoca (de micro-ondas)", Tempo = "3", Potencia = 7, Instrucao = "Observar o barulho de estouros do milho, caso houver um intervalo de mais de 10 segundos entre um estouro e outro, interrompa o aquecimento." },
                    new Programa { Nome = "Leite", Alimento = "Leite", Tempo = "5", Potencia = 5, Instrucao = "Cuidado com aquecimento de líquidos, o choque térmico aliado ao movimento do recipiente pode causar fervura imediata causando risco de queimaduras." },
                    new Programa { Nome = "Carnes de boi", Alimento = "Carne em pedaço ou fatias", Tempo = "14", Potencia = 4, Instrucao = "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme." },
                    new Programa { Nome = "Frango", Alimento = "Frango (qualquer corte)", Tempo = "8", Potencia = 7, Instrucao = "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme." },
                    new Programa { Nome = "Feijão", Alimento = "Feijão congelado", Tempo = "8", Potencia = 9, Instrucao = "Deixe o recipiente destampado e em casos de plástico, cuidado ao retirar o recipiente pois o mesmo pode perder resistência em altas temperaturas." }
                };

                foreach (var programa in programasPreDefinidos)
                {
                    var caractereDisponivel = _programaService.ObterCaractereDisponivel();

                    if (caractereDisponivel == null)
                    {
                        break;
                    }

                    programa.Caractere = caractereDisponivel;
                    programa.Customizado = false;
                    Programas.Add(programa);
                }

                SaveChanges();
            }
        }
    }
}
