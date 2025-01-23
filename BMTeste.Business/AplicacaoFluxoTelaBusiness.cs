using BMTeste.Domain.Models;
using BMTeste.Domain.Models.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace BMTeste.Business
{
    public partial class AplicacaoBusiness
    {
        private void MenuPrincipal()
        {
            bool _existeArquivo = _sistemaDeArquivos.ExisteArquivoDados();

            if (_sistemaDeArquivos.ExisteArquivoDados())
            {

            }
            else
            {
                Inclusao(_existeArquivo);
            }

        }


        private void Inclusao(bool existeArquivo)
        {
            if (!existeArquivo) Write("\nNão foi encontrado o arquivo de dados das rotas\nÉ possivel incluir manualmente ou importar um arquivo.");
            int opcaoIncluirArquivo = Perguntar("\n1Escolha [1] para inclusao manual ou [2] para importacao de arquivo : ", typeof(int), false);
            switch (opcaoIncluirArquivo)
            {
                case 1:
                    InclusaoManual();
                    break;
                case 2:
                    ImportacaoArquivo();
                    break;
            };

        }

        private void ImportacaoArquivo()
        {
            bool _resultadoImportacao = false;
            WriteLine("Importacao de arquivo de rotas");
            while(true)
            {
                string _caminhoArquivoImportar = Perguntar("\rEntre com o caminho do arquivo :", typeof(string), false);
                bool = _sistemaDeArquivos..
            }
        }

        private void InclusaoManual()
        {
            bool _resultadoInclusao = false;
            WriteLine("Inclusao Manual de rotas");
            while (true)
            {
                string _siglaOrigem = Perguntar("\rEntre com a sigla origem :",typeof(string),false);
                string _siglaDestino = Perguntar("Entre com a sigla destino :",typeof(string),false);
                decimal _valorRota = Perguntar("Entre com o valor da rota :", typeof(decimal), false);
                bool _incluir = Perguntar($"A rota é {_siglaOrigem},{_siglaDestino},{_valorRota} confirma (S/N)? :",typeof(bool),false);
                if (_incluir)
                {
                    _resultadoInclusao = _rotaBusiness.IncluirRota(new Rota
                    {
                        Origem = _siglaOrigem,
                        Destino = _siglaDestino,
                        Valor = _valorRota
                    });

                    string _mensagem = _resultadoInclusao ? "A rota foi inserida!" : "Erro ao inserir a rota";
                    WriteLine(_mensagem);
                }
                else
                {
                    WriteLine("A rota foi descartada!");
                }
                bool _continuar = Perguntar("Deseja incluir uma nova rota (S/N)? :", typeof(bool), false);
                if (!_continuar) break;
            }
        }

        private static dynamic Perguntar(string Pergunta, Type tipoRetorno, bool AceitarNuloOuVazio = true)
        {
            dynamic resultado;
            while (true)
            {
                Write(Pergunta);
                string resposta = ReadLine();
                if (!AceitarNuloOuVazio && string.IsNullOrEmpty(resposta)) continue;
                try
                {
                    resultado = resposta;
                    if (tipoRetorno == typeof(int)) resultado = Convert.ToInt32(resposta);
                    if (tipoRetorno == typeof(decimal)) resultado = Convert.ToDecimal(resposta);
                    if (tipoRetorno == typeof(bool)) resultado = resposta.Equals("S", StringComparison.CurrentCultureIgnoreCase) ? true : false;
                    break;
                }
                catch
                {
                    WriteLine("Entrada invalida, tente novamente");
                }
            }
            return resultado;
        }
    }
}
