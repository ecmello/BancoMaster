using BMTeste.Domain.Models;
using BMTeste.Domain.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTeste.Application.Business
{
    public class OperacoesArquivoDadosBusiness : IOperacoesArquivoDadosBusiness
    {
        private readonly IOperacoesArquivoDadosFileSystem _fileSystem;

        public OperacoesArquivoDadosBusiness(IOperacoesArquivoDadosFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public bool ApagarArquivoDados()
        {
            return _fileSystem.ApagarArquivoDados();
        }

        public string[] CarregarArquivoDados(string? caminhoArquivo = null)
        {
            return _fileSystem.CarregarArquivoDados(caminhoArquivo);
        }

        public bool ExisteArquivoDados(string? caminhoArquivo = null)
        {
            return _fileSystem.ExisteArquivoDados(caminhoArquivo);
        }

        public bool GravarArquivoDados(IEnumerable<Rota> rotas)
        {
            IEnumerable<string> _linhas = new List<string>();
            foreach (var _rota in rotas)
                _linhas = _linhas.Append(ConverterParaLinha(_rota));

            return _fileSystem.GravarArquivoDados(_linhas.ToArray());
        }

        public bool ImportarArquivo(string? caminhoArquivo = null)
        {
            bool resultado = false;
            string[] linhasArquivo = _fileSystem.CarregarArquivoDados(caminhoArquivo);
            if(linhasArquivo.Length > 0)
            {
                resultado = _fileSystem.GravarArquivoDados(linhasArquivo);
            }
            return resultado;
        }


        private string ConverterParaLinha(Rota rota)
        {
            return string.Join(',', new object[]
            {
                rota.Origem,
                rota.Destino,
                rota.Valor
            });
        }

    }
}
