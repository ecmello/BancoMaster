using BMTeste.Domain.Models;
using BMTeste.Domain.Models.Interface;
using static System.Console;

namespace BMTeste.Application.Business
{
    public partial class AplicacaoBusiness

    {
        private readonly IRotaBusiness _rotaBusiness;
        private readonly IOperacoesArquivoDadosBusiness _sistemaDeArquivos;
        private bool Execute = true;
        private Rota RotaDesejadaValidada = new Rota();

        public AplicacaoBusiness(IRotaBusiness rotaBusiness, IOperacoesArquivoDadosBusiness sistemaDeArquivos)
        {
            _rotaBusiness = rotaBusiness;
            _sistemaDeArquivos = sistemaDeArquivos;
        }

        public void Inicio()
        {
            while (Execute) FluxoPrincipal();
        }

    }
}
