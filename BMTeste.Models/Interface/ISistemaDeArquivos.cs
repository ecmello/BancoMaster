using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BMTeste.Domain.Models.Interface
{
    public interface ISistemaDeArquivos
    {
        bool ExisteArquivoDados();
        bool ApagarArquivoDados();
        bool GravarArquivoDados(IEnumerable<Rota> rotas);
        string[] CarregarArquivoDados();
        bool ImportarArquivo(string caminhoArquivo);
    }
}
