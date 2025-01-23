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

namespace BMTeste.Application.Business

{
    public partial class AplicacaoBusiness
    {
        private void FluxoPrincipal()
        {
            bool _existeArquivo = _sistemaDeArquivos.ExisteArquivoDados();
            WriteLine("Banco Master | Teste DEV |");
            if (_existeArquivo)
            {
                MenuPesquisarIncluir();
            }
            else
            {
                Incluir(_existeArquivo);
            }

        }

        private void MenuPesquisarIncluir()
        {
            WriteLine("ROTAS DISPONIVEIS:");
            foreach(var _rota in _rotaBusiness.Rotas)
            {
                WriteLine($"{_rota.Origem},{_rota.Destino},{_rota.Valor}");
            }
            int _opcaoPesquisarIncluir = Perguntar("\nENTRE COM :\n[1] Para pesquisar a melhor rota\n[2] Para incluir uma nova rota\nEscolha : ", typeof(int), false);
            switch (_opcaoPesquisarIncluir)
            {
                case 1:
                    Pesquisar();
                    break;
                case 2:
                    InclusaoManual();
                    break;
            };
        }

        private void Pesquisar()
        {
            bool _pesquisar = true;
            string _mensagem = string.Empty;

            while (_pesquisar)
            {
                bool _entrarDadosPesquisar = true;
                while (_entrarDadosPesquisar)
                {
                    WriteLine("Insira a rota no formato XXX-XXX ou pressione [ENTER] para voltar.");
                    string _entradaRotaDesejada = Perguntar("Digite a rota desejada :", typeof(string), true);
                    if (string.IsNullOrEmpty(_entradaRotaDesejada))
                    {
                        _pesquisar = false;
                        break;
                    }

                    string[] DadosEntrada = _entradaRotaDesejada.Split('-');

                    (bool ValidarEntrarDados, string MensagemValidarEntrada, Rota RotaDesejadaValidada) _validacaoEntrada = _rotaBusiness.ValidarEntrada(DadosEntrada);
                    _entrarDadosPesquisar = _validacaoEntrada.ValidarEntrarDados;
                    if (_entrarDadosPesquisar)
                    {
                        WriteLine(_validacaoEntrada.MensagemValidarEntrada);
                    }
                    else
                    {
                        RotaDesejadaValidada = _validacaoEntrada.RotaDesejadaValidada;
                    }
                }
                if (!_pesquisar) continue;
                Trajeto MelhorRota = _rotaBusiness.ObterMelhorRota(RotaDesejadaValidada);
                if(MelhorRota != null) { 
                var OrigensDoTrajeto = MelhorRota.RotasDoTrajeto.Select(r => r.Origem).ToList().ToArray();
                _mensagem = $"Melhor Rota {string.Join(" - ", OrigensDoTrajeto)} - {RotaDesejadaValidada.Destino} ao custo de $ {MelhorRota.custo}";
                }
                else
                {
                    _mensagem = "Não há rotas disponiveis para a origem/destino informadas";
                }
                WriteLine(_mensagem);
            }
        }

        private void Incluir(bool existeArquivo)
        {
            if (!existeArquivo) WriteLine("\nNão foi encontrado o arquivo de dados das rotas\nÉ possivel incluir manualmente ou importar um arquivo.");
            int _opcaoIncluirArquivo = Perguntar("\n[1] para inclusao manual\n[2] para importacao de arquivo\nEscolha : ", typeof(int), false);
            switch (_opcaoIncluirArquivo)
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
            string _caminhoArquivoImportar = Perguntar("\rEntre com o caminho do arquivo :", typeof(string), false);
            _resultadoImportacao = _sistemaDeArquivos.ImportarArquivo(_caminhoArquivoImportar);
            string _mensagem = _resultadoImportacao ? "Arquivo importado com sucesso" : "Falha ao importar o arquivo";
            Write(_mensagem);
        }

        private void InclusaoManual()
        {
            bool _resultadoInclusao = false;
            WriteLine("Inclusao Manual de rotas");
            while (true)
            {
                string _siglaOrigem = Perguntar("\rEntre com a sigla origem :", typeof(string), false);
                string _siglaDestino = Perguntar("Entre com a sigla destino :", typeof(string), false);
                decimal _valorRota = Perguntar("Entre com o valor da rota :", typeof(decimal), false);
                bool _incluir = Perguntar($"A rota é {_siglaOrigem},{_siglaDestino},{_valorRota} confirma (S/N)? :", typeof(bool), false);
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
                string? resposta = ReadLine();
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
