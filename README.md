# Banco Master Teste Dev
Sistema que elege a melhor rota entre aeroportos no critério de menor custo.
***
</br>
##Como executar a aplicação##
Após clonar o repositório, basta apenas digitar na linha de comando :  

`...\BMTeste\>dotnet run --project .\BMTesteConsoleApp\BMTeste.ConsoleApp.csproj`  

Para executar os testes digite na linha de comandos :</br>

`...>dotnet test`  
***
</br>
##Estrutura dos arquivos/pacotes##
A estrutura dos arquivos segue o padrão DDD, com um executável mínimo apenas para servir como entry point e configurar a inversão de controle dos serviços.
***
</br>
##TO DO :##
- Incluir verificação de integridade à arquivos a importar, com check de colunas e tipos de dados no arquivo para recusa em caso de estar não-conforme.
- Incluir verificação de dados quando da inclusão manual de uma nova rota.
- Incluir funcionalidade de alterar rota cadastrada
- Incluir funcionadadade de excluir rota cadastrada

***
</br>
##Considerações:##
```
Reconheci tardiamente que a escolha de um aplicativo console para a camada de apresentação não foi a melhor opção, porém fui induzido a tal pelo exemplo disposto na descrição do teste. Acreditei ainda que seria uma interface com usuário muito simples, porém a medida que as funcionalidades eram testadas, verifiquei que mais desenvolvimento seria exigido para que a entrega fosse minimamente funcional.
```


