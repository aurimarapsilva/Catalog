using Catalog.Core.Commands;
using Catalog.Core.Entities;
using Catalog.Core.Handlers.Contracts;
using Catalog.Core.Repositories;
using Catalog.Core.Response;

namespace Catalog.Core.Handlers
{
    /// <summary>
    /// Manipuladora dos comandos recebidos sobre marca
    /// </summary>
    public class HandlerCatalogBrand :
        IHandler<CommandAddBrand>,
        IHandler<CommandUpdateBrand>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public HandlerCatalogBrand(ICatalogBrandRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Varivael repositorio
        /// </summary>
        private readonly ICatalogBrandRepository _repository;

        /// <summary>
        /// Método responsável por manipular o comando de adicionar uma marca
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public IResponse handle(CommandAddBrand command)
        {
            // validação rapida
            command.Validate();
            if (command.Invalid)
                return new ResponseError<CommandAddBrand>(
                    errorCode: "400",
                    description: "Não foi possível prosseguir com a solicitação",
                    data: command.Notifications
                );

            // criar a entidade de marcas
            var brand = new CatalogBrand(brand: command.Brand);

            // faz a persistencia dos dados no banco de dados e retorna false caso apresentar algum erro
            if (!_repository.CreateBrandAsync(brand: brand))
                return new ResponseError<CommandAddBrand>(
                    errorCode: "400",
                    description: "Não foi possível prosseguir com a solicitação, pois foi apresentado um erro sistemico",
                    data: null
                );

            // retorna sucesso na operação para adicionar um item no estoque de produto
            //return new ResponseOk<CatalogBrand>(brand);
            return new ResponseOk<CatalogBrand>(brand);
        }

        /// <summary>
        /// Método responsável por manipular o comando de alterar uma marca
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public IResponse handle(CommandUpdateBrand command)
        {
            // validação rapida
            command.Validate();
            if (command.Invalid)
                return new ResponseError<CommandUpdateBrand>(
                    errorCode: "400",
                    description: "Não foi possível prosseguir com a solicitação",
                    data: command.Notifications
                );

            // localiza uma marca e verifica se obteve sucesso na localização
            var brand = _repository.CatalogBrandIdAsync(id: command.Id).Result;
            if (brand == null)
                return new ResponseError<CommandAddBrand>(
                        errorCode: "400",
                        description: "Não foi possível prosseguir com a solicitação, pois não foi localizado a marca desejada",
                        data: null
                    );

            // edita os dados da marca do produto
            brand.UpdateCatalogBrand(brand: command.Brand);

            // faz a persistencia dos dados no banco de dados e retorna false caso apresentar algum erro
            if (!_repository.UpdateBrandAsync(brand: brand))
                return new ResponseError<CommandAddBrand>(
                    errorCode: "400",
                    description: "Não foi possível prosseguir com a solicitação, pois foi apresentado um erro sistemico",
                    data: null
                );

            // retorna sucesso na operação para alterar uma marca
            return new ResponseOk<CatalogBrand>(brand);
        }
    }
}