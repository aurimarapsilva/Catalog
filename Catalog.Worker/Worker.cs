using catalog.infra.DataContext;
using catalog.infra.RabbitMQ;
using catalog.infra.Repositories;
using catalog.infra.Services;
using Catalog.Core.Commands;
using Catalog.Core.Commands.Contracts;
using Catalog.Core.Handlers;
using Catalog.Core.Handlers.Contracts;
using Catalog.Core.Repositories;
using Catalog.Core.Response;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IRabbitMQPersistentConnection _persistentConnection;
        private IResponse _result;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _persistentConnection = new RabbitMQPersistentConnection("localhost");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var mensagem = "<Transacao><tipoTransacao>ADDBRAND</tipoTransacao><brand>Teste</brand></Transacao>";   //_persistentConnection.Receivement("teste", false, false, false);
            _logger.LogInformation("Mensagem Recebida");
            if (string.IsNullOrEmpty(mensagem))
            {
                _logger.LogWarning("Mensagem recebida esta em branco");
                return;
            }

            _logger.LogInformation("Inicio do fluxo de recebimento");
            DescobrirNome(mensagem);
            _logger.LogInformation("Fim do fluxo de recebimento");

        }

        public void DescobrirNome(string msg)
        {
            var tipoTransacao = TratarMenssagem.RecuperarValor(msg, "tipoTransacao");

            switch (tipoTransacao.ToUpper())
            {
                case "ADDBRAND":
                    _logger.LogInformation("O tipo de transacao é addbrand");
                    _logger.LogInformation("Iniciando fluxo");
                    _result = ServiceWorker.AddBrand(msg);
                    if (!_result.HasError())
                    {
                        _persistentConnection.Send("teste", false, false, false, "ok");
                        _logger.LogInformation("A nova marca foi adicionada com sucesso");
                    }
                    else
                    {
                        _logger.LogWarning("Erro ao adicionar uma marca");
                        _persistentConnection.Send("teste", false, false, false, "erro");
                    }
                    break;
                case "ADDITEM":

                    break;
                case "ADDTYPE":

                    break;
                case "REGISTERITEM":

                    break;
                case "REMOVEITEM":

                    break;
                case "UPDATEBRAND":

                    break;
                case "UPDATEITEM":

                    break;
                case "UPDATETYPE":

                    break;
                default:
                    _logger.LogWarning("tipoTransacao incorreta");
                    break;
            }
        }
    }
}
