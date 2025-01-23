using BMTeste.Domain.Models.Interface;
using BMTeste.CrossCutting.IOC;
using Microsoft.Extensions.DependencyInjection;

namespace BMTeste.Test
{
    public class ValidacaoArquivoDados : IClassFixture<DispositivoDeTeste>
    {

        private readonly IOperacoesArquivoDadosFileSystem _sistemaDeArquivos;

        public ValidacaoArquivoDados(DispositivoDeTeste dispositivo)
        {
            _sistemaDeArquivos = dispositivo.ServiceProvider.GetRequiredService<IOperacoesArquivoDadosFileSystem>();
        }

        [Fact]
        public void VerificaExistenciaArquivoDados()
        {
            bool arquivoExiste = _sistemaDeArquivos.ExisteArquivoDados();
            Assert.True(arquivoExiste);
        }

        [Fact]
        public void VerificaInexisteniciaExistenciaArquivoDados()
        {
            //Se arquivo existir, carregar um copia em memoria e deletar

            //executar o test
            bool arquivoExiste = _sistemaDeArquivos.ExisteArquivoDados();
            Assert.False(_sistemaDeArquivos.ExisteArquivoDados(), $"O arquivo não existe.");

            //regravar o arquivo
        }


        
    }
}