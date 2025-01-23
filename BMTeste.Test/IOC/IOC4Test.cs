using BMTeste.Application.Business;
using BMTeste.Domain.Models.Interface;
using BMTeste.Infrastructure.CrossCutting.SistemaDeArquivos;
using BMTeste.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace BMTeste.Test.IOC
{
    public class IOC4Test : IDisposable
    {
        public ServiceProvider ServiceProvider { get; private set; }

        public IOC4Test()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IRotaBusiness, RotaBusiness>();
            serviceCollection.AddTransient<IRotaRepository, RotaRepository>();
            serviceCollection.AddTransient<IOperacoesArquivoDadosBusiness, OperacoesArquivoDadosBusiness>();
            serviceCollection.AddTransient<IOperacoesArquivoDadosFileSystem, OperacoesArquivoDadosFileSystem>();
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public void Dispose()
        {
            ServiceProvider?.Dispose();
        }
    }
}
