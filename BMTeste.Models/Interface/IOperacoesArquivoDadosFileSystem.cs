namespace BMTeste.Domain.Models.Interface
{
    public interface IOperacoesArquivoDadosFileSystem
    {
        bool ExisteArquivoDados(string? caminhoArquivo = null);
        bool ApagarArquivoDados(string? caminhoArquivo = null);
        bool GravarArquivoDados(string[] linhas, string? caminhoArquivo = null);
        string[] CarregarArquivoDados(string? caminhoArquivo = null);
    }
}
