using BMTeste.Application.Business;
using BMTeste.Domain.Models.Interface;
using BMTeste.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTeste.CrossCutting.IOC
{
    public class DispositivoDeTeste : IDisposable
    {
        public ServiceProvider ServiceProvider { get; private set; }

        public DispositivoDeTeste()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IRotaBusiness, RotaBusiness>();
            serviceCollection.AddTransient<IRotaRepository, RotaRepository>();
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public void Dispose()
        {
            ServiceProvider?.Dispose();
        }
    }
}
