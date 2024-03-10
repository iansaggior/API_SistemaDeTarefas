# SistemaDeTarefas

Observações para rodar o projeto:
- verificar as configurações do banco de dados.
  Foi Utilizado o SQL Server
- no arquivo "appsettings.json" verificar se suas credenciais estão ok
- para criar o banco de dados, ir no Console do Gerenciador de Pacotes e rodar os seguintes comandos:
  - para criar a Migration ->    Add-Migration InitialDB -Context SistemaTarefasDBContext
  - para criar o banco de dados de acordo com as configurações ->    Update-Database -Context SistemaTarefasDBContext

após esse processo já será possível rodar o programa e acessar o endereço padrão do Swagger
