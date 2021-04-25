using System.Net;
using System.Threading.Tasks;
using Catalog.Core.Commands;
using Catalog.Core.Entities;
using Catalog.Core.Handlers;
using Catalog.Core.Queries;
using Catalog.Core.Queries.Contracts;
using Catalog.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    [Produces("application/json")]
    public class BrandController : ControllerBase
    {
        /// <summary>
        /// Endpoint responsible for listing the brands
        /// </summary>
        /// <param name="repository"> </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize"> </param>
        /// <response code="200">
        /// Exemplo:
        ///
        ///     Get /v1/Brand
        ///     {
        ///        "pagIndex": 1,
        ///        "pagSize": 10,
        ///        "count": 2,
        ///        "data" : [
        ///             {
        ///                 "id" : 1,
        ///                 "brand" : "test brand"
        ///             },
        ///             {
        ///                 "id" : 2,
        ///                 "brand" : "test brand 2"
        ///             }
        ///         ]
        ///     }
        ///</response>
        /// <response code="400">
        /// Exemplo:
        ///
        ///     Get /v1/Brand
        ///     { }
        ///</response>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GenericResultQuery), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CatalogBrandsAsync([FromServices] ICatalogBrandRepository repository, [FromQuery] int pageSize, [FromQuery] int pageIndex)
        {
            var result = await repository.CatalogBrandsAsync(pageSize: pageSize, pageIndex: pageIndex);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        /// <summary>
        /// Endpoint responsible for displaying the brand according to the given id
        /// </summary>
        /// <param name="id"> </param>
        /// <param name="repository"> </param>
        /// <response code="200">
        /// Exemplo:
        ///
        ///     Get /v1/Brand/1
        ///     {
        ///         "id" : 1,
        ///         "brand" : "test brand"
        ///     }
        ///</response>
        /// <response code="400">
        /// Exemplo:
        ///
        ///     Get /v1/Brand/1
        ///     { }
        ///</response>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CatalogBrand), (int)HttpStatusCode.OK)]
        [Route("id")]
        public async Task<IActionResult> CatalogBrandIdAsync([FromServices] ICatalogBrandRepository repository, int id)
        {
            var result = await repository.CatalogBrandIdAsync(id: id);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        /// <summary>
        /// Endpoint responsible for creating a new brand
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="command">
        /// Descrição:
        ///
        ///     Brand :                                                                         
        ///                                                                                                                         {
        ///                                                                                                                             "obrigatorio" : "true",    
        ///                                                                                                                             "tamanhoMinimo" : "1 caracter",
        ///                                                                                                                             "tamanhoMáximo" : "100 caracteres"
        ///                                                                                                                         }
        ///</param>
        /// <response code="200">
        /// Exemplo:
        ///
        ///     Post /v1/Brand
        ///     {
        ///         "success" : true,
        ///         "message" : "a nova marca foi adicionada com sucesso!",
        ///         "data" : {
        ///                     "id" : 1,
        ///                     "brand" : "test brand"
        ///                  }
        ///     }
        ///</response>
        /// <response code="400">
        /// Exemplo:
        ///
        ///     Post /v1/Brand
        ///     {
        ///         "success" : false,
        ///         "message" : "Não foi possível prosseguir com a solicitação, pois foi apresentado um erro sistemico",
        ///         "data" : null
        ///     }
        ///</response>
        [HttpPost]
        [ProducesResponseType(typeof(GenericResultCommand), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GenericResultCommand), (int)HttpStatusCode.OK)]
        public IActionResult CommandAddBrand([FromServices] HandlerCatalogBrand handler, [FromBody] CommandAddBrand command)
        {
            var result = (GenericResultCommand)handler.handle(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Endpoint responsible for editing a brand
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="id"></param>
        /// <param name="command">
        /// Descrição:
        ///
        ///     Id :
        ///                                                                                                                         {
        ///                                                                                                                             "obrigatorio" : "true",    
        ///                                                                                                                             "tamanhoMinimo" : "",
        ///                                                                                                                             "tamanhoMáximo" : ""
        ///                                                                                                                         }
        ///     Brand :                                                                         
        ///                                                                                                                         {
        ///                                                                                                                             "obrigatorio" : "true",    
        ///                                                                                                                             "tamanhoMinimo" : "1 caracter",
        ///                                                                                                                             "tamanhoMáximo" : "100 caracteres"
        ///                                                                                                                         }
        ///</param>
        /// <response code="200">
        /// Exemplo:
        ///
        ///     Put /v1/Brand/1
        ///     {
        ///         "success" = true,
        ///         "message" : "a marca foi atualizada com sucesso!",
        ///         "data" : {
        ///                     "id" : 1,
        ///                     "brand" : "test brand"
        ///                  }
        ///     }
        ///</response>
        /// <response code="400">
        /// Exemplo:
        ///
        ///     Put /v1/Brand
        ///     {
        ///         "success" : false,
        ///         "message" : "Não foi possível prosseguir com a solicitação, pois foi apresentado um erro sistemico",
        ///         "data" : null
        ///     }
        ///</response>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GenericResultCommand), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GenericResultCommand), (int)HttpStatusCode.OK)]
        [Route("id")]
        public IActionResult CommandUpdateBrand([FromServices] HandlerCatalogBrand handler, [FromBody] CommandUpdateBrand command, int id)
        {
            if (id != command.Id)
                return BadRequest("informed ids are divergent,(id informados estão divergentes)");

            var result = (GenericResultCommand)handler.handle(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}