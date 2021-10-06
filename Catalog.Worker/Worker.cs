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

                //if (!string.IsNullOrEmpty(mensagem))
                //    IniciaTransacao(mensagem);

                mensagem = string.Empty;
                _persistentConnection.DisposeMessage();

                await Task.Delay(_messageRange, stoppingToken);
            }
        }

        public void IniciaTransacao(string mensagem)
        {
            _logger.LogInformation("Inicio do fluxo de recebimento");
            DescobrirNome(mensagem);
            _logger.LogInformation("Fim do fluxo de recebimento");
        }

        public void DescobrirNome(string msg)
        {
            var tipoTransacao = TratarMenssagem.RecuperarValor(msg, "tipoTransacao");
            _logger.LogInformation("Iniciando fluxo");

            switch (tipoTransacao.ToUpper())
            {
                case "ADDBRAND":
                    _logger.LogInformation("ADDBRAND");
                    _result = _serviceWorker.AddBrand(msg);
                    if (!_result.HasError())
                    {
                        _logger.LogInformation("A nova marca foi adicionada com sucesso");
                        _persistentConnection.Send(_queue, false, false, false, $"({TratarMenssagem.RecuperarValor(msg, "numeroSolicitação")})  ok");
                    }
                    else
                    {
                        _logger.LogWarning("Erro ao adicionar uma marca");
                        _persistentConnection.Send(_queue, false, false, false, $"({TratarMenssagem.RecuperarValor(msg, "numeroSolicitação")})  erro");
                    }
                    break;
                case "ADDITEM":
                    _logger.LogInformation("ADDITEM");
                    _result = _serviceWorker.AddItem(msg);
                    if (!_result.HasError())
                    {
                        _logger.LogInformation("Um novo produto foi adicionado no estoque");
                        _persistentConnection.Send(_queue, false, false, false, "ok");
                    }
                    else
                    {
                        _logger.LogWarning("Erro ao incluir um novo produto no estoque");
                        _persistentConnection.Send(_queue, false, false, false, "erro");
                    }
                    break;
                case "ADDTYPE":
                    _logger.LogInformation("ADDTYPE");
                    _result = _serviceWorker.AddType(msg);
                    if (!_result.HasError())
                    {
                        _logger.LogInformation("O novo tipo foi adicionado com sucesso");
                        _persistentConnection.Send(_queue, false, false, false, "ok");
                    }
                    else
                    {
                        _logger.LogWarning("Erro ao adicionar um tipo");
                        _persistentConnection.Send(_queue, false, false, false, "erro");
                    }
                    break;
                case "REGISTERITEM":
                    _logger.LogInformation("REGISTERITEM");
                    _result = _serviceWorker.RegisterItem(msg);
                    if (!_result.HasError())
                    {
                        _logger.LogInformation("Foi registrado um novo produto na base de dados");
                        _persistentConnection.Send(_queue, false, false, false, "ok");
                    }
                    else
                    {
                        _logger.LogWarning("Erro em registrar produto");
                        _persistentConnection.Send(_queue, false, false, false, "erro");
                    }
                    break;
                case "REMOVEITEM":
                    _logger.LogInformation("REMOVEITEM");
                    _result = _serviceWorker.RemoveItem(msg);
                    if (!_result.HasError())
                    {
                        _logger.LogInformation("Produto removido com sucesso");
                        _persistentConnection.Send(_queue, false, false, false, "ok");
                    }
                    else
                    {
                        _logger.LogWarning("Erro ao remover um item");
                        _persistentConnection.Send(_queue, false, false, false, "erro");
                    }
                    break;
                case "UPDATEBRAND":
                    _logger.LogInformation("UPDATEBRAND");
                    _result = _serviceWorker.UpdateBrand(msg);
                    if (!_result.HasError())
                    {
                        _logger.LogInformation("A  marca foi atualizada com sucesso");
                        _persistentConnection.Send(_queue, false, false, false, "ok");
                    }
                    else
                    {
                        _logger.LogWarning("Erro ao atualizar uma marca");
                        _persistentConnection.Send(_queue, false, false, false, "erro");
                    }
                    break;
                case "UPDATEITEM":
                    _logger.LogInformation("UPDATEITEM");
                    _result = _serviceWorker.UpdateItem(msg);
                    if (!_result.HasError())
                    {
                        _logger.LogInformation("O produto foi atualizado com sucesso");
                        _persistentConnection.Send(_queue, false, false, false, "ok");
                    }
                    else
                    {
                        _logger.LogWarning("Erro ao atualizar um produto");
                        _persistentConnection.Send(_queue, false, false, false, "erro");
                    }
                    break;
                case "UPDATETYPE":
                    _logger.LogInformation("UPDATETYPE");
                    _result = _serviceWorker.UpdateType(msg);
                    if (!_result.HasError())
                    {
                        _logger.LogInformation("O tipo de produto foi atualizado com sucesso");
                        _persistentConnection.Send(_queue, false, false, false, "ok");
                    }
                    else
                    {
                        _logger.LogWarning("Erro ao atualizar um tipo de produto");
                        _persistentConnection.Send(_queue, false, false, false, "erro");
                    }
                    break;
                default:
                    _logger.LogWarning("tipoTransacao incorreta");
                    break;
            }
        }
    }
}
