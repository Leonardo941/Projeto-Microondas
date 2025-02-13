namespace Microondas.Models
{
    public class Programa
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Alimento { get; set; }
        public string? Tempo { get; set; }
        public int? Potencia { get; set; }
        public string? Instrucao { get; set; }
        public bool Customizado { get; set; }
        public string? Caractere { get; set; }
    }
}
