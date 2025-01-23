using BMTeste.Domain.Models;

namespace BMTeste.Domain.Models.Interface
{
    public interface IOperacoesArquivoDadosBusiness
    {
        bool ExisteArquivoDados(string? caminhoArquivo = null);
        bool ApagarArquivoDados();
        bool GravarArquivoDados(IEnumerable<Rota> rotas);
        string[] CarregarArquivoDados(string? caminhoArquivo = null);
        bool ImportarArquivo(string? caminhoArquivo = null);
    }
}
