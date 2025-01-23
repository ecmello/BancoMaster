using BMTeste.Domain.Models.Interface;

namespace BMTeste.Infrastructure.CrossCutting.SistemaDeArquivos
{
    public class OperacoesArquivoDadosFileSystem : IOperacoesArquivoDadosFileSystem
    {
        private static string ArquivoDados = $"{AppDomain.CurrentDomain.BaseDirectory}\\{System.Configuration.ConfigurationManager.AppSettings["NomeDoArquivoDeDados"]}";

        public bool ApagarArquivoDados(string? caminhoArquivo = null)
        {
            ArquivoDados = caminhoArquivo != null ? caminhoArquivo : ArquivoDados;
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

        public string[] CarregarArquivoDados(string? caminhoArquivo = null)
        {
            List<string> resultado = new List<string>();
            ArquivoDados = caminhoArquivo != null ? caminhoArquivo : ArquivoDados;
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

        public bool ExisteArquivoDados(string? caminhoArquivo = null)
        {
            ArquivoDados = caminhoArquivo != null ? caminhoArquivo : ArquivoDados;
            return File.Exists(ArquivoDados);
        }

        public bool GravarArquivoDados(string[] linhas, string? caminhoArquivo = null)
        {
            bool _resultado = false;
            ArquivoDados = caminhoArquivo != null ? caminhoArquivo : ArquivoDados;
            try
            {
                using (StreamWriter sw = new StreamWriter(ArquivoDados, append: true))
                {
                    foreach(var _linha in linhas) sw.WriteLine(_linha);
                }
                _resultado = true;
            }
            catch
            {
                //não é necessário fazer nada
            }
            return _resultado;
        }
    }
}
