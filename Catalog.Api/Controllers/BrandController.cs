using Catalog.Core.Commands;
using Catalog.Core.Entities;
using Catalog.Core.Handlers;
using Catalog.Core.Repositories;
using Catalog.Core.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace catalog.api.Controllers
{
    /// <summary>
    /// Controller referente a marcas de produtos
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class BrandController : Controller
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
        [ProducesResponseType(typeof(IList<CatalogBrand>), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> CatalogBrandsAsync([FromServices] ICatalogBrandRepository repository, [FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var request = repository.CatalogBrandsAsync(pageSize, pageIndex);
            if (request.Result.HasError())
                return BadRequest(request.Result.Error());

            return Ok(new { Count = request.Result.Count(), Data = request.Result.ResponseObj() });

        }

        /// <summary>
        /// Cria uma nova marca 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(CatalogBrand), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> CreateBrand([FromBody] CommandAddBrand command, [FromServices] HandlerCatalogBrand handler)
        {
            var request = handler.handle(command);
            if (request.HasError())
                return BadRequest(request.Error());

            return Ok(request.ResponseObj());
        }

        /// <summary>
        /// Atualiza uma marca de acordo com os parametros
        /// </summary>
        /// <param name="command"></param>
        /// <param name="handler"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(CatalogBrand), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> UpdateBrand([FromBody] CommandUpdateBrand command, [FromServices] HandlerCatalogBrand handler,int id)
        {
            if(id != command.Id)
                return BadRequest(new { Code = "400", Description = "O Id do parametro e do body estão divergentes"});

            var request = handler.handle(command);
            if (request.HasError())
                return BadRequest(request.Error());

            return Ok(request.ResponseObj());
        }
    }
}
