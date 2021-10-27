using System;
using catalog.infra.RabbitMQ;

namespace catalog.test.Worker
{
    public class RabbitMQPersistentConnection : IRabbitMQPersistentConnection
    {
        public string Receivement(string queue, bool durable, bool exclusive, bool autoDelete)
        {
            switch (queue)
            {
                case "catalog1":
                    return "<root><Thread>33459</Thread><Transacao><TipoTransacao>AddBrand</TipoTransacao><Brand>Teste</Brand></Transacao></root>";
                case "catalog2":
                    return "<root><Thread>14208</Thread><Transacao><TipoTransacao>AddItem</TipoTransacao><ProductId>1</ProductId><Quantity>20</Quantity></Transacao></root>";
                case "catalog3":
                    return "<root><Thread>81655</Thread><Transacao><TipoTransacao>AddType</TipoTransacao><Type>Teste</Type></Transacao></root>";
                case "catalog4":
                    return "<root><Thread>23704</Thread><Transacao><TipoTransacao>RegisterItem</TipoTransacao><Name>Teste</Name><Description>Descrição Teste</Description><Price>2.99</Price><CatalogTypeId>1</CatalogTypeId><CatalogBrandId>1</CatalogBrandId><RestockThreshold>15</RestockThreshold><MaxStockThreshold>35</MaxStockThreshold></Transacao></root>";
                case "catalog5":
                    return "<root><Thread>98417</Thread><Transacao><TipoTransacao>RemoveItem</TipoTransacao><ProductId>5</ProductId><Quantity>5</Quantity></Transacao></root>";
                case "catalog6":
                    return "<root><Thread>39356</Thread><Transacao><TipoTransacao>UpdateBrand</TipoTransacao><BrandId>1</BrandId><Brand>Teste</Brand></Transacao></root>";
                case "catalog7":
                    return "<root><Thread>23704</Thread><Transacao><TipoTransacao>UpdateItem</TipoTransacao><ProductId>1</ProductId><Name>Testes</Name><Description>Descrição Teste</Description><Price>2.99</Price><CatalogTypeId>1</CatalogTypeId><CatalogBrandId>1</CatalogBrandId><RestockThreshold>15</RestockThreshold><MaxStockThreshold>35</MaxStockThreshold></Transacao></root>";
                case "catalog8":
                    return "<root><Thread>81655</Thread><Transacao><TipoTransacao>UpdateType</TipoTransacao><TypeId>1</TypeId><Type>Teste</Type></Transacao></root>";
                case "catalog9":
                    return "<root><Thread>76136</Thread><Transacao><TipoTransacao>AddBrand</TipoTransacao><Brand>Teste com mais de 100 caracteres Teste com mais de 100 caracteres Teste com mais de 100 caracteres Teste com mais de 100 caracteres Teste com mais de 100 caracteres</Brand></Transacao></root>;";
                case "catalog10":
                    return "<root><Thread>75393</Thread><Transacao><TipoTransacao>AddItem</TipoTransacao><ProductId>1</ProductId><Quantity>0</Quantity></Transacao></root>;";
                case "catalog11":
                    return "<root><Thread>12329</Thread><Transacao><TipoTransacao>AddType</TipoTransacao><Type>Teste com mais de 100 caracteres Teste com mais de 100 caracteres Teste com mais de 100 caracteres Teste com mais de 100 caracteres Teste com mais de 100 caracteres</Type></Transacao></root>;";
                case "catalog12":
                    return "<root><Thread>83259</Thread><Transacao><TipoTransacao>RegisterItem</TipoTransacao><Name>Teste</Name><Description>Descrição Teste</Description><Price>-1</Price><CatalogTypeId>1</CatalogTypeId><CatalogBrandId>1</CatalogBrandId><RestockThreshold>15</RestockThreshold><MaxStockThreshold>35</MaxStockThreshold></Transacao></root>;";
                case "catalog13":
                    return "<root><Thread>68113</Thread><Transacao><TipoTransacao>RemoveItem</TipoTransacao><ProductId>5</ProductId><Quantity>0</Quantity></Transacao></root>;";
                case "catalog14":
                    return "<root><Thread>50995</Thread><Transacao><TipoTransacao>UpdateBrand</TipoTransacao><BrandId>0</BrandId><Brand>Teste</Brand></Transacao></root>;";
                case "catalog15":
                    return "<root><Thread>28757</Thread><Transacao><TipoTransacao>UpdateItem</TipoTransacao><ProductId>0</ProductId><Name>Testes</Name><Description>Descrição Teste</Description><Price>2.99</Price><CatalogTypeId>1</CatalogTypeId><CatalogBrandId>1</CatalogBrandId><RestockThreshold>15</RestockThreshold><MaxStockThreshold>35</MaxStockThreshold></Transacao></root>;";
                case "catalog16":
                    return "<root><Thread>82237</Thread><Transacao><TipoTransacao>UpdateType</TipoTransacao><TypeId>0</TypeId><Type>Teste</Type></Transacao></root>;";
                default:
                    return "Error";
            }
        }

        public string Send(string queue, bool durable, bool exclusive, bool autoDelete, string message) => "Enviado";

        public string GetMessage() => string.Empty;

        public void DisposeMessage() { }
    }
}
