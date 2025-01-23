using BMTeste.Domain.Models;
using BMTeste.Domain.Models.Interface;


namespace BMTeste.Infrastructure.Data
{
    public class RotaRepository : IRotaRepository
    {
        private readonly ISistemaDeArquivos _sistemaDeArquivos;
        private IEnumerable<Rota> _rotas;
        public IEnumerable<Rota> Rotas { get { return _rotas; } }

        public RotaRepository(ISistemaDeArquivos sistemaDeArquivos)
        {
            _sistemaDeArquivos = sistemaDeArquivos;
            _rotas = ObterRotas(_sistemaDeArquivos.ExisteArquivoDados());
        }

        public bool IncluirRota(Rota rota)
        {
            bool resultado = false;
            _rotas = _rotas.Append(rota);
            if (_sistemaDeArquivos.ApagarArquivoDados())
            {
                resultado = _sistemaDeArquivos.GravarArquivoDados(_rotas);
            }
            _rotas = ObterRotas(_sistemaDeArquivos.ExisteArquivoDados());
            return resultado;
        }
            

        private IEnumerable<Rota> ObterRotas(bool ObrigatoriedadeCarregarArquivo)
        {
            IEnumerable<Rota> resultado = new List<Rota>();
            if (ObrigatoriedadeCarregarArquivo)
            {
                if (_rotas == null || _rotas.Count() == 0)
                {
                    string[] linhas = _sistemaDeArquivos.CarregarArquivoDados();
                    foreach (string linha in linhas)
                        resultado = resultado.Append(ConverterParaRota(linha));
                }
                else
                {
                    resultado = _rotas;
                }
            }
            return resultado;
        }

        private Rota ConverterParaRota(string linha)
        {
            string[] dados = linha.Split(',');
            return new Rota
            {
                Origem = dados[0],
                Destino = dados[1],
                Valor = Convert.ToDecimal(dados[2])
            };
        }

    }
}
