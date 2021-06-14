using Catalog.Core.Commands;
using Catalog.Core.Entities;
using Catalog.Core.Handlers;
using Catalog.Core.Repositories;
using Catalog.Core.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace catalog.api.Controllers
{
    /// <summary>
    /// Gerencia os fluxos de produtos
    /// </summary>
    [ApiController]
    [Route("v1/[controller]")]
    [Produces("application/json")]
    public class ItemController : Controller
    {
        /// <summary>
        /// Realiza uma query(consulta) com base nos parametros
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(IList<CatalogItem>), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public IActionResult GetItemsByIdsAsync([FromServices] ICatalogItemRepository repository, [FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var request = repository.GetItemsByIdsAsync(pageSize, pageIndex);
            if (request.Result.HasError())
                return BadRequest(request.Result.Error());

            return Ok(new { Count = request.Result.Count(), Data = request.Result.ResponseObj() });
        }


        /// <summary>
        /// Cria uma novo produto
        /// </summary>
        /// <param name="command"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(CatalogItem), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public IActionResult CreateProduct([FromBody] CommandRegisterItem command, [FromServices] HandlerCatalogItem handler)
        {
            var request = handler.handle(command);
            if (request.HasError())
                return BadRequest(request.Error());

            return Ok(request.ResponseObj());
        }

        /// <summary>
        /// Atualiza um produto de acordo com os parametros
        /// </summary>
        /// <param name="command"></param>
        /// <param name="handler"></param>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(CatalogItem), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public IActionResult UpdateProduct([FromBody] CommandUpdateItem command, [FromServices] HandlerCatalogItem handler, int id)
        {
            if (id != command.Id)
                return BadRequest(new { Code = "400", Description = "O Id do parametro e do body estão divergentes" });

            var request = handler.handle(command);
            if (request.HasError())
                return BadRequest(request.Error());

            return Ok(request.ResponseObj());
        }

        /// <summary>
        /// Realiza uma query(consulta) com base nos parametros
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("name")]
        [ProducesResponseType(typeof(IList<CatalogItem>), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public IActionResult ItemsWithNameAsync([FromServices] ICatalogItemRepository repository, string name, int pageSize = 10, int pageIndex = 0)
        {
            var request = repository.ItemsWithNameAsync(name, pageSize, pageIndex);
            if (request.Result.HasError())
                return BadRequest(request.Result.Error());

            return Ok(new { Count = request.Result.Count(), Data = request.Result.ResponseObj() });
        }


        /// <summary>
        /// Realiza uma query(consulta) com base nos parametros
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("brandAndType")]
        [ProducesResponseType(typeof(IList<CatalogItem>), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public IActionResult ItemsByTypeIdAndBrandIdAsync([FromServices] ICatalogItemRepository repository, [FromQuery] int catalogTypeId, [FromQuery] int? catalogBrandId, [FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var request = repository.ItemsByTypeIdAndBrandIdAsync(catalogTypeId, catalogBrandId, pageSize, pageIndex);
            if (request.Result.HasError())
                return BadRequest(request.Result.Error());

            return Ok(new { Count = request.Result.Count(), Data = request.Result.ResponseObj() });
        }

        /// <summary>
        /// Realiza uma query(consulta) com base nos parametros
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("brand")]
        [ProducesResponseType(typeof(IList<CatalogItem>), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public IActionResult ItemsByBrandIdAsync([FromServices] ICatalogItemRepository repository, [FromQuery] int? catalogBrandId, [FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var request = repository.ItemsByBrandIdAsync(catalogBrandId, pageSize, pageIndex);
            if (request.Result.HasError())
                return BadRequest(request.Result.Error());

            return Ok(new { Count = request.Result.Count(), Data = request.Result.ResponseObj() });
        }

        /// <summary>
        /// Realiza uma query(consulta) com base nos parametros
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("type")]
        [ProducesResponseType(typeof(IList<CatalogItem>), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public IActionResult ItemsByTypeIdAsync([FromServices] ICatalogItemRepository repository, [FromQuery] int? catalogTypeId, [FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var request = repository.ItemsByTypeIdAsync(catalogTypeId, pageSize, pageIndex);
            if (request.Result.HasError())
                return BadRequest(request.Result.Error());

            return Ok(new { Count = request.Result.Count(), Data = request.Result.ResponseObj() });
        }

        /// <summary>
        /// Adiciona uma nova quantidade no estoque do produto
        /// </summary>
        /// <param name="command"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("addProduct")]
        [ProducesResponseType(typeof(CatalogItem), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public IActionResult AddProduct([FromBody] CommandAddItem command, [FromServices] HandlerCatalogItem handler)
        {
            var request = handler.handle(command);
            if (request.HasError())
                return BadRequest(request.Error());

            return Ok(request.ResponseObj());
        }

        /// <summary>
        /// Remove uma quantidade no estoque do produto
        /// </summary>
        /// <param name="command"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("removeProduct")]
        [ProducesResponseType(typeof(CatalogItem), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public IActionResult RemoveProduct([FromBody] CommandRemoveItem command, [FromServices] HandlerCatalogItem handler)
        {
            var request = handler.handle(command);
            if (request.HasError())
                return BadRequest(request.Error());

            return Ok(request.ResponseObj());
        }
    }
}
