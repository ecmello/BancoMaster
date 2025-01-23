using BMTeste.Domain.Models.Interface;
using Microsoft.Extensions.DependencyInjection;
using BMTeste.Test.IOC;

namespace BMTeste.Test
{
    public class OperacoesArquivoDadosFileSystemTest : IClassFixture<IOC4Test>
    {

        private readonly IOperacoesArquivoDadosFileSystem _fileSystem;
        private string ArquivoDados = $"{AppDomain.CurrentDomain.BaseDirectory}\\rotas.csv";

        public OperacoesArquivoDadosFileSystemTest(IOC4Test InjecaoDependenciaTests)
        {
            _fileSystem = InjecaoDependenciaTests.ServiceProvider.GetRequiredService<IOperacoesArquivoDadosFileSystem>();
        }

        #region ExisteArquivoDados
        [Fact]
        public void ExisteArquivoDadosTeste()
        {
            bool arquivoExiste = _fileSystem.ExisteArquivoDados(ArquivoDados);
            Assert.True(arquivoExiste);
        }

        [Fact]
        public void ExisteArquivoDadosContraTeste()
        {
            //preparacao para teste
            File.Copy(ArquivoDados, $"{ArquivoDados}.bkp");
            File.Delete(ArquivoDados);

            //Teste
            bool _arquivoExiste = _fileSystem.ExisteArquivoDados(ArquivoDados);
            Assert.False(_arquivoExiste);

            //finalizacao do teste
            File.Copy($"{ArquivoDados}.bkp", ArquivoDados);
            File.Delete($"{ArquivoDados}.bkp");

        }
        #endregion

        #region CarregarArquivoDados
        [Fact]
        public void CarregarArquivoDadosTeste()
        {
            string[] _carga = _fileSystem.CarregarArquivoDados(ArquivoDados);
            Assert.True(_carga.Length > 0);
        }
        #endregion

        #region ApagarArquivoDados
        [Fact]
        public void ApagarArquivoDadosTest()
        {
            //Realizar backup
            File.Copy(ArquivoDados, $"{ArquivoDados}.bkp");

            bool _resultadoApagarArquivo = _fileSystem.ApagarArquivoDados(ArquivoDados);
            bool _arquivoExiste = File.Exists(ArquivoDados);
            Assert.True(_resultadoApagarArquivo && !_arquivoExiste);

            File.Copy($"{ArquivoDados}.bkp", ArquivoDados);
            File.Delete($"{ArquivoDados}.bkp");
        }
        #endregion

        #region GravarArquivoDados
        [Theory]
        [InlineData((object) new string[] { "GRU,BRC,10", "BRC,SCL,5", "GRU,CDG,75", "GRU,SCL,20", "GRU,ORL,56", "ORL,CDG,5", "SCL,ORL,20" })]
        public void GravarArquivoDadosTest(string[] linhas)
        {
            File.Copy(ArquivoDados, $"{ArquivoDados}.bkp");
            File.Delete(ArquivoDados);

            bool _resultadoGravarArquivo = _fileSystem.GravarArquivoDados(linhas);
            bool _arquivoExiste = File.Exists(ArquivoDados);
            string[] _carga = _fileSystem.CarregarArquivoDados(ArquivoDados);
            Assert.True(_arquivoExiste && _resultadoGravarArquivo && _carga.Length == linhas.Length);

            File.Delete($"{ArquivoDados}.bkp");
        }
        #endregion
    }

}