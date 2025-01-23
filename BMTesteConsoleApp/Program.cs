using BMTeste.Application.Business;
using BMTeste.Infrastructure.CrossCutting.IOC;
using Microsoft.Extensions.DependencyInjection;


namespace BMTeste.Presentation.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var services = InjecaoDependencia.ConfigureServices();
            AplicacaoBusiness app = services.GetRequiredService<AplicacaoBusiness>();
            app.Inicio();
        }
    }

}