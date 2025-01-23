using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BMTeste.Domain.Models.Interface
{
    public interface IOperacoesArquivoDadosFileSystem
    {
        bool ExisteArquivoDados(string? caminhoArquivo = null);
        bool ApagarArquivoDados();
        bool GravarArquivoDados(string[] linhas);
        string[] CarregarArquivoDados(string? caminhoArquivo = null);
    }
}
