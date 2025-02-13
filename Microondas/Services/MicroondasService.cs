using Newtonsoft.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microondas.Services
{
    public class MicroondasService
    {
        private int _tempoRestante;
        private int _potencia;
        private bool _emAquecimento;
        private bool _taskEmExecucao;
        private CancellationTokenSource _cancelTokenSource;

        public MicroondasService()
        {
            _tempoRestante = 0;
            _potencia = 0;
            _emAquecimento = false;
            _taskEmExecucao = false;
            _cancelTokenSource = new CancellationTokenSource();
        }

        public void Aquecer(int tempo, int potencia)
        {
            ValidarTempoPotencia(tempo, potencia);

            _tempoRestante = tempo;
            _potencia = potencia;
            _emAquecimento = true;
            _cancelTokenSource = new CancellationTokenSource();

            Task.Run(() => ProcessoAquecimento(_cancelTokenSource.Token));
        }

        public void ValidarTempoPotencia(int tempo, int potencia)
        {
            if (tempo < 1 || tempo > 120)
                throw new ArgumentException("Tempo inválido! Informe um valor entre 1s e 120s.");

            if (potencia < 1 || potencia > 10)
                throw new ArgumentException("Potência inválida! Informe um valor entre 1 e 10.");
        }

        public void Pausar()
        {
            if (_emAquecimento)
            {
                _emAquecimento = false;
            }
            else
            {
                _emAquecimento = true;
                _cancelTokenSource = new CancellationTokenSource();
                Task.Run(() => ProcessoAquecimento(_cancelTokenSource.Token));
            }
        }

        public void Cancelar()
        {
            _cancelTokenSource.Cancel();
            _emAquecimento = false;
            _tempoRestante = 0;
        }

        public string ObterStatus()
        {
            var status = new { aquecimento = _emAquecimento, tempoRestante = _tempoRestante };

            return JsonConvert.SerializeObject(status);
        }

        private async Task ProcessoAquecimento(CancellationToken token)
        {
            if (_taskEmExecucao)
                return;

            _taskEmExecucao = true;

            while (_tempoRestante > 0 && !token.IsCancellationRequested)
            {
                await Task.Delay(1000);
                _tempoRestante--;
            }

            if (!token.IsCancellationRequested)
            {
                _emAquecimento = false;
            }

            _taskEmExecucao = false;
        }
    }

}
