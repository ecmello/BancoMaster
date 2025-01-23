using BMTeste.Application.Business;
using BMTeste.Domain.Models.Interface;
using BMTeste.Infrastructure.CrossCutting.SistemaDeArquivos;
using BMTeste.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

namespace BMTeste.Infrastructure.CrossCutting.IOC
{
    public static class InjecaoDependencia
    {
        public static ServiceProvider ConfigureServices()
        {
            var serviceProvider = new ServiceCollection()
                .AddScoped<IRotaBusiness,RotaBusiness>()
                .AddScoped<IRotaRepository,RotaRepository>()
                .AddSingleton<IOperacoesArquivoDadosBusiness, OperacoesArquivoDadosBusiness>()
                .AddSingleton<IOperacoesArquivoDadosFileSystem, OperacoesArquivoDadosFileSystem>()
                .AddSingleton<AplicacaoBusiness>(x => new AplicacaoBusiness(x.GetRequiredService<IRotaBusiness>(), x.GetRequiredService<IOperacoesArquivoDadosBusiness>()))
                .BuildServiceProvider();
            return serviceProvider;
        }
    }
}
