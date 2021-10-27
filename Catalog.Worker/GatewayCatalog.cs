using System;
using Catalog.Core.Response;
using catalog.infra.RabbitMQ;
using catalog.infra.Services;

namespace Catalog.Worker
{
    public class GatewayCatalog
    {
        private readonly IServiceWorker _serviceWorker;
        public string LogMessage { get; private set; }
        public string SendMessage { get; private set; }
        
        public GatewayCatalog(IServiceWorker serviceWorker) => _serviceWorker = serviceWorker;
        
        public IResponse BeginTransaction(string msg) => _serviceWorker.BeginTransaction(msg);

        public bool HasError(string msg, IResponse value)
        {
            if (value == null)
            {
                LogMessage = $"{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff")} " +
                             $"[GTW_CATALOG -> GTW]   " +
                             $"({Message.FindValue(msg, "numeroSolicitação")}): " +
                             $"<result>" +
                             $"<status>999</status>" +
                             $"<message>Erro desconhecido</message>" +
                             $"</result>";

                SendMessage = "<root>" +
                              $"<numeroSolicitação>{Message.FindValue(msg, "numeroSolicitação")}</numeroSolicitação>" +
                              "<result>" +
                              $"<status>999</status>" +
                              $"<message>Erro desconhecido</message>" +
                              "</result>" +
                              "</root>";
                return true;
            }
            else if (value.HasError())
            {
                LogMessage = $"{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff")} " +
                       $"[GTW_CATALOG -> GTW]   " +
                       $"({Message.FindValue(msg, "numeroSolicitação")}): " +
                       $"<result>" +
                       $"<status>{value.Error().Code}</status>" +
                       $"<message>{value.Error().Description}</message>" +
                       $"</result>";

                SendMessage = "<root>" +
                               $"<numeroSolicitação>{Message.FindValue(msg, "numeroSolicitação")}</numeroSolicitação>" +
                               "<result>" +
                               $"<status>{value.Error().Code}</status>" +
                               $"<message>{value.Error().Description}</message>" +
                               "</result>" +
                               "</root>";
                return true;
            }
            else
            {
                LogMessage = $"{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff")} " +
                       "[GTW_CATALOG -> GTW]   " +
                       $"({Message.FindValue(msg, "numeroSolicitação")}): " +
                       "<result>" +
                       "<status>000</status>" +
                       "<message>Sucesso</message>" +
                       "</result>";

                SendMessage = "<root>" +
                               $"<numeroSolicitação>{Message.FindValue(msg, "numeroSolicitação")}</numeroSolicitação>" +
                               "<result>" +
                               "<status>000</status>" +
                               "<message>Sucesso</message>" +
                               "</result>" +
                               "</root>";
                return false;
            }
        }
    }
}
