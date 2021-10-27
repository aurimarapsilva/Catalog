# Catalog

Projeto realizado para integragração com os produtos

O objetivo deste projeto é cadastrar, atulizar e excluir produtos e controlar estoque.

Foi construindo uma API REST, utilizamos os seguintes recursos como:

- Camadas -> Core, Infrastructure, Worker
- Patter Utilizado no Core -> CQRS
- Testes Unitarios -> MSTest
- RabbitMQ
- Banco de dados -> SQL Server

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

## RabbitMQ

O RabbitMQ é um software de mensagens com código aberto, que implementou o protocolo "Advanced Message Queuing Protocol" (AMQP), que foi estendido com uma arquitetura de plug-in para suportar o protocolo "Streaming Text Oriented Messaging Protocol" (STOMP), o MQTT entre outros protocolos.

![image](https://raw.githubusercontent.com/ZoeStyle/Catalog/master/RabbitMQ.png)

---
