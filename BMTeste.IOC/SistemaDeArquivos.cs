using BMTeste.Domain.Models;
using BMTeste.Domain.Models.Interface;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.IO;
using System.Net.NetworkInformation;
namespace BMTeste.IOC
{
    public class SistemaDeArquivos : ISistemaDeArquivos
    {
        private static string ArquivoDados = $"{AppDomain.CurrentDomain.BaseDirectory}\\{System.Configuration.ConfigurationManager.AppSettings["NomeDoArquivoDeDados"].ToString()}";

        public bool ApagarArquivoDados()
        {
            bool resultado = false;
            try
            {
                File.Delete(ArquivoDados);
                resultado = true;
            }
            catch
            {
                //não é necessário fazer nada
            }
            return resultado;
        }

        public string[] CarregarArquivoDados()
        {
            List<string> resultado = new List<string>();
            string? linha = null;
            using (StreamReader sr = new StreamReader(ArquivoDados))
            {
                linha = sr.ReadLine();
                while (linha != null)
                {
                    resultado.Add(linha);
                    linha = sr.ReadLine();
                }
            }
            return resultado.ToArray();
        }

        public bool ExisteArquivoDados()
        {
            return File.Exists(ArquivoDados);
        }

        public bool GravarArquivoDados(IEnumerable<Rota> rotas)
        {
            bool _resultado = false;
            IEnumerable<string> _linhas = new List<string>();
            foreach (var _rota in rotas)
            {
                _linhas = _linhas.Append(ConverterParaLinha(_rota));
            }
            try
            {
                using (StreamWriter sw = new StreamWriter(ArquivoDados, append: true))
                {
                    foreach(var linha in _linhas) sw.WriteLine(linha);
                }
                _resultado = true;
            }
            catch (Exception ex)
            {
                //não é necessário fazer nada
            }
            return _resultado;
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
