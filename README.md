# Catalog

Projeto realizado para integragração com os produtos

O objetivo deste projeto é cadastrar, atulizar e excluir produtos e controlar estoque.

Foi construindo uma API REST, utilizamos os seguintes recursos como:

- Swagger
- Camadas -> Core, Infrastructure, API
- CQRS
- MsTest -> Para testes unitários

Mas quais funcionalidades foram implementadas?

- Cadastra produtos
- Atualizar produto
- Excluir produto
- Consultar produtos
- Consultar pelo nome do produto
- Consultar pela marca do produto
- Consultar pelo tipo do produto
- Adicionar produto no estoque
- Remover produto no estoque
- Cadatrar marca do produto
- Atualizar marca do produto
- Cadatrar tipo do produto
- Atualizar tipo do produto

---

## Swagger

Swagger é uma linguagem de descrição de interface para descrever APIs RESTful expressas usando JSON . O Swagger é usado junto com um conjunto de ferramentas de software de código aberto para projetar, construir, documentar e usar serviços da Web RESTful . O Swagger inclui documentação automatizada, geração de código (em muitas linguagens de programação) e geração de casos de teste.

![image](https://raw.githubusercontent.com/ZoeStyle/Catalog/master/Swagger.png)

---

## CQRS

~~~ csharp
Command-Query Responsability Segregation
~~~

Como o nome já diz, é sobre separar a responsabilidade de escrita e leitura de seus dados.

CQRS é um pattern, um padrão arquitetural assim como Event Sourcing, Transaction Script e etc. O CQRS não é um estilo arquitetural como desenvolvimento em camadas, modelo client-server, REST e etc.

![image](https://raw.githubusercontent.com/ZoeStyle/Catalog/master/cqrs.png)

---

## MsTest

É o conjunto de ferramentas de teste de unidade da Microsoft integrado em algumas versões do Visual Studio 2005 e posteriores. 

A estrutura de teste de unidade é definida em Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll. 
Os testes de unidade criados com a estrutura de teste de unidade podem ser executados no Visual Studio ou, usando MSTest.exe, a partir de uma linha de comando.

![image](https://raw.githubusercontent.com/ZoeStyle/Catalog/master/MsTest.png)

---

## Microsoft SQL Server

Como um Banco de dados, é um produto de software cuja principal função é a de armazenar e recuperar dados solicitados por outras aplicações de software, seja aqueles no mesmo computador ou aqueles em execução em outro computador através de uma rede (incluindo a Internet).

![image](https://raw.githubusercontent.com/ZoeStyle/Catalog/master/SqlServer.png)

---

## Flunt

Cada aplicativo tem regras de negócios e validações, e você provavelmente precisará manter todos os erros e notificações que aconteceram e enviá-los para algum lugar, talvez para sua IU.

Flunt implementa o Padrão de Notificação e ajuda você a rastrear tudo o que acontece, consolidando suas notificações e tornando-o fácil de acessar e manipular.
