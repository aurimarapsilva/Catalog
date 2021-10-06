using catalog.infra.RabbitMQ;
using catalog.infra.Services;
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
        private string _queue;
        private IServiceWorker _serviceWorker;
        private int _messageRange = 100;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _persistentConnection = new RabbitMQPersistentConnection("localhost");
            _queue = "catalog";
            _serviceWorker = new ServiceWorker();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"URI  {"localhost"}");
            _logger.LogInformation($"Parametro de Execução  {_queue}");
            _logger.LogInformation($"Aguardando mensagens");

            int valor = 0;

            while (!stoppingToken.IsCancellationRequested)
            {
                valor++;

                //var mensagem = _persistentConnection.Send(_queue, false, false, false, $"<root><numeroSolicitação>{valor}</numeroSolicitação><Transacao><tipoTransacao>ADDBRAND</tipoTransacao><brand>Teste</brand></Transacao></root>");
                //_logger.LogInformation(
                //    $"{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff")} " +
                //    $"[GTW -> GTW_CATALOG]   " +
                //    $"({TratarMenssagem.RecuperarValor(mensagem, "numeroSolicitação")}): " +
                //    $"{TratarMenssagem.RecuperarValor(mensagem, "Transacao")}"
                //    );

                _persistentConnection.Receivement(_queue, false, false, false);
                var mensagem = _persistentConnection.GetMessage();
                _logger.LogInformation(
                    $"{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff")} " +
                    $"[GTW_CATALOG <- GTW]   " +
                    $"({TratarMenssagem.RecuperarValor(mensagem, "numeroSolicitação")}): " +
                    $"{TratarMenssagem.RecuperarValor(mensagem, "Transacao")}"
                    );

                if (!string.IsNullOrEmpty(mensagem))
                    IniciaTransacao(mensagem);

                mensagem = string.Empty;
                _persistentConnection.DisposeMessage();

                await Task.Delay(_messageRange, stoppingToken);
            }
        }

        public void IniciaTransacao(string mensagem)
        {
            DescobrirNome(mensagem);
        }

        private void HasError(string msg, IResponse value)
        {
            if (!value.HasError())
            {
                _logger.LogInformation(
                    $"{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff")} " +
                    $"[GTW_CATALOG -> GTW]   " +
                    $"({TratarMenssagem.RecuperarValor(msg, "numeroSolicitação")}): " +
                    $"<result><status>000</status><message>Sucesso</message></result>"
                    );
                _persistentConnection.Send(_queue, false, false, false,
                    $"<root>" +
                        $"<numeroSolicitação>{TratarMenssagem.RecuperarValor(msg, "numeroSolicitação")}</numeroSolicitação>" +
                        $"<result>" +
                            $"<status>000</status>" +
                            $"<message>Sucesso</message>" +
                        $"</result>" +
                    $"</root>");
            }
            else
            {
                _logger.LogWarning($"{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff")} " +
                    $"[GTW_CATALOG -> GTW]   " +
                    $"({TratarMenssagem.RecuperarValor(msg, "numeroSolicitação")}): " +
                    $"<result>" +
                        $"<status>{value.Error().Code}</status>" +
                            $"<message>{value.Error().Description}</message>" +
                    $"</result>"
                    );
                _persistentConnection.Send(_queue, false, false, false,
                    $"<root>" +
                        $"<numeroSolicitação>{TratarMenssagem.RecuperarValor(msg, "numeroSolicitação")}</numeroSolicitação>" +
                        $"<result>" +
                            $"<status>{value.Error().Code}</status>" +
                            $"<message>{value.Error().Description}</message>" +
                        $"</result>" +
                    $"</root>");
            }

        }

        public void DescobrirNome(string msg)
        {
            var tipoTransacao = TratarMenssagem.RecuperarValor(msg, "tipoTransacao");
            _logger.LogInformation("Iniciando fluxo");

            switch (tipoTransacao.ToUpper())
            {
                case "ADDBRAND":
                    _result = _serviceWorker.AddBrand(msg);
                    HasError(msg, _result);
                    break;
                case "ADDITEM":
                    _result = _serviceWorker.AddItem(msg);
                    HasError(msg, _result);
                    break;
                case "ADDTYPE":
                    _result = _serviceWorker.AddType(msg);
                    HasError(msg, _result);
                    break;
                case "REGISTERITEM":
                    _result = _serviceWorker.RegisterItem(msg);
                    HasError(msg, _result);
                    break;
                case "REMOVEITEM":
                    _result = _serviceWorker.RemoveItem(msg);
                    HasError(msg, _result);
                    break;
                case "UPDATEBRAND":
                    _result = _serviceWorker.UpdateBrand(msg);
                    HasError(msg, _result);
                    break;
                case "UPDATEITEM":
                    _result = _serviceWorker.UpdateItem(msg);
                    HasError(msg, _result);
                    break;
                case "UPDATETYPE":
                    _result = _serviceWorker.UpdateType(msg);
                    HasError(msg, _result);
                    break;
                default:
                    _logger.LogWarning($"{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff")} " +
                    $"[GTW_CATALOG -> GTW]   " +
                    $"({TratarMenssagem.RecuperarValor(msg, "numeroSolicitação")}): " +
                    $"<result>" +
                        $"<status>999</status>" +
                            $"<message>Tipo de transação incorreta</message>" +
                    $"</result>"
                    );
                    _persistentConnection.Send(_queue, false, false, false,
                        $"<root>" +
                            $"<numeroSolicitação>{TratarMenssagem.RecuperarValor(msg, "numeroSolicitação")}</numeroSolicitação>" +
                            $"<result>" +
                                $"<status>999</status>" +
                                    $"<message>Tipo de transação incorreta</message>" +
                            $"</result>" +
                        $"</root>");
                    break;
            }
        }
    }
}
