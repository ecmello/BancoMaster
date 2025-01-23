using BMTeste.Application.Business;
using BMTeste.Business;
using BMTeste.Domain.Models.Interface;
using BMTeste.Infrastructure.Data;
using BMTeste.IOC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTeste.Infrastructure.CrossCutting.IOC
{
    public static class InjecaoDependencia
    {
        public static ServiceProvider ConfigureServices()
        {
            var serviceProvider = new ServiceCollection()
                .AddScoped<IRotaBusiness,RotaBusiness>()
                .AddScoped<IRotaRepository,RotaRepository>()
                .AddSingleton<ISistemaDeArquivos,SistemaDeArquivos>()
                .AddSingleton<AplicacaoBusiness>(x => new AplicacaoBusiness(x.GetRequiredService<IRotaBusiness>(), x.GetRequiredService<ISistemaDeArquivos>()))
                .BuildServiceProvider();
            return serviceProvider;
        }
    }
}
