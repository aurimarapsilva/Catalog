using catalog.infra.RabbitMQ;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using catalog.infra.Services;

namespace Catalog.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IRabbitMQPersistentConnection _persistentConnection;
        private const string _queue = "catalog";
        private int _messageRange = 1000;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _persistentConnection = new RabbitMQPersistentConnection("localhost");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int valor = 0;

            while (!stoppingToken.IsCancellationRequested)
            {
                _persistentConnection.Receivement(_queue, false, false, false);
                var message = _persistentConnection.GetMessage();

                if (!string.IsNullOrEmpty(message))
                {
                    _logger.LogInformation(
                        $"{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff")} " +
                        $"[GTW_CATALOG <- GTW]   " +
                        $"({Message.FindValue(message, "numeroSolicitação")}): " +
                        $"{Message.FindValue(message, "Transacao")}"
                    );

                    ResultTransaction(message);
                }

                message = CleanUp();

                await Task.Delay(_messageRange, stoppingToken);
            }
        }

        private void ResultTransaction(string message)
        {
            var serviceWorker = new ServiceWorker();
            var gateway = new GatewayCatalog(serviceWorker);

            var resultTransaction = gateway.BeginTransaction(message);
            if (!gateway.HasError(message, resultTransaction))
            {
                _logger.LogInformation(gateway.LogMessage);
                _persistentConnection.Send(_queue, false, false, false, gateway.SendMessage);
            }
            else
            {
                _logger.LogWarning(gateway.LogMessage);
                _persistentConnection.Send(_queue, false, false, false, gateway.SendMessage);
            }
        }

        private string CleanUp()
        {
            _persistentConnection.DisposeMessage();
            return string.Empty;
        }
    }
}