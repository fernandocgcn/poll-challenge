# Poll Challenge

Implementação em C# (ASP.NET Core) de uma API RESTful moderna e autônoma, com troca de informações em formato JSON, para utilização de uma enquete, na qual as pessoas respondem uma pergunta escolhendo dentre algumas alternativas pré-definidas.

![](/misc/ClassDiagram.png)

![](/misc/ComponentDiagram.png)

## Tecnologias Utilizadas

### Banco de Dados (SQLite - Lib: Microsoft.EntityFrameworkCore.Sqlite)

* Utilizei um banco de dados simples e leve para demonstrar como a aplicação adapta-se a qualquer tipo de database relacional, modificando apenas a biblioteca de acesso ao banco na API. A simplicidade e rapidez na implantação do DB nesse caso são mais relevantes para a demonstração do projeto, pois não há necessidade de avaliar a escalabilidade do banco.

### Back-End (C# - ASP.NET Core)

* src/PollChallenge.Model - Biblioteca do modelo de dados do projeto (entidades). Acesso aos dados com Entity Framework Core, utilizando Migrations (Microsoft.EntityFrameworkCore.IEntityTypeConfiguration) e o padrão Unit of Work (Microsoft.EntityFrameworkCore.DbContext).  

* src/PollChallenge.Api - Web API RESTful contendo os services e controllers (domínio e lógica do negócio), utilizando os padrões MVVM, MVC e Injeção de Dependência para Inversão de Controle dos serviços utilizados. Documentação automática usando NSwag (uma implementação da especificação OpenAPI/Swagger para .NET e TypeScript).  

* src/PollChallenge.Tests - Testes de unidade (MSTest) dos services e controllers (domínio e lógica do negócio) do projeto da API. Fundamental para a utilização de TDD no desenvolvimento do projeto e ajudar a manter um baixo acoplamento.  

### Metodologias, Técnicas e Princípios Utilizados

* SOLID, Design Patterns, DDD, TDD, ER Model, OOP, KISS, DRY

## Screenshots da Aplicação

![](/misc/screenshots/01.png)
![](/misc/screenshots/02.png)
![](/misc/screenshots/03.png)
![](/misc/screenshots/04.png)
![](/misc/screenshots/05.png)

## Considerações sobre o Projeto

1. Qual foi a parte mais difícil na solução do desafio?  
R. Não que tenha sido difícil, mas a parte um pouco mais elaborada, foi na concepção do modelo de classes, tive uma dúvida de como implementar a chave primária da classe Option (opções da enquete).  Primeiramente implementei como chave simples (uma única propriedade da classe), mas no decorrer do desenvolvimento, verifiquei que o endpoint (post) para registrar	 um voto para uma opção necessitava do id da enquete (via query) e do id da opção (via body), então modifiquei a chave primária para composta contendo tanto o id da opção como o id da enquete (chave estrangeira).  

2. Se você pudesse voltar no tempo e se dar um conselho no início do desafio, qual seria?  
R. O conselho seria: "Inclua tanto uma associação da classe Option para Poll como também uma da classe Poll para Option (coleção de opções)", pois além do EFCore facilitar muito a navegação entre as entidades, a modificação trouxe um código mais simples e limpo na resolução dos requisitos.  

3. Se você pudesse mudar algo na proposta/definição do desafio, o que seria?  
R. Achei o desafio muito bem detalhado e autoexplicativo, talvez uma pequena modificação seja na especificação do id das opções da enquete. Se único somente dentro da enquete ou único no total.  

## Tutorial de Instalação no Windows para Desenvolvedores

### Pre-requisitos

[Instalar o SDK do .NET Core 3.1 mais recente](https://dotnet.microsoft.com/download/dotnet-core/3.1) no computador que irá gerar o pacote de instalação da aplicação no servidor, ou seja, onde está o código-fonte do projeto.  

Para verificar se o computador já possui o .NET Core 3.1 instalado, na janela do terminal (prompt de comando), execute o comando a seguir:

```
dotnet --version
```

No servidor onde irá hospedar a aplicação:  

1. Habilitar o IIS:
No Windows, navegue até Painel de Controle > Programas > Programas e Recursos > Ativar ou desativar recursos do Windows (lado esquerdo da tela).  
Selecione a caixa de seleção Serviços de Informações da Internet. Selecione OK.  

2. [Instalar o Hosting Bundle](https://dotnet.microsoft.com/download/dotnet-core/3.1).

### Publicação

Com o SDK devidamente instalado, vá para a pasta onde estão os projetos da aplicação (poll-challenge\src), para compilar a aplicação, ler as dependências especificadas nos arquivos dos projetos, e publicar o conjunto de arquivos resultante em um diretório (<OUTPUT_DIRECTORY>), execute o comando a seguir:

```
dotnet publish –o <OUTPUT_DIRECTORY>
```

Depois de gerado, copie o diretório <OUTPUT_DIRECTORY> para o servidor.

### Configuração do IIS

1. Primeiro criar um Pool de Aplicativos para os sites em ASP.NET Core, para isto abra o IIS, vá em Pools de Aplicativos e clique em "Adicionar Pool de Aplicativos..."  

2. Defina o nome, e selecione "Sem Código Gerenciado" para a Versão do .NET CLR, isto porque o Hosting Bundle que instalamos irá fazer o gerenciamento da aplicação e clique em OK.  

3. Para criar a aplicação basta clicar com o botão direito em Default Web Site e depois em "Adicionar Aplicativo...".

4. Defina o Alias (nome da aplicação), selecione o Pool de Aplicativos criado anteriormente e a pasta onde está o conjunto de arquivos resultante da publicação no Caminho físico.  

Pronto, você já pode acessar a API!

## Autor

* **Fernando Cordeiro Gondim Cavalcante Neto** - [fernandocgcn@GitHub](https://github.com/fernandocgcn) e [fernandocgcn@LinkedIn](https://www.linkedin.com/in/fernandocgcn)
