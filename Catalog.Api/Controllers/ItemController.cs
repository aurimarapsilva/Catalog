using System.Net;
using System.Threading.Tasks;
using Catalog.Core.Queries;
using Catalog.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    [Produces("application/json")]
    public class ItemController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GenericResultQuery), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetItemsByIdsAsync([FromServices] ICatalogItemRepository repository, [FromQuery] int pageSize, [FromQuery] int pageIndex)
        {
            var result = await repository.GetItemsByIdsAsync(pageSize: pageSize, pageIndex: pageIndex);

            if (result == null)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GenericResultQuery), (int)HttpStatusCode.OK)]
        [Route("id")]
        public async Task<IActionResult> ItemByIdAsync([FromServices] ICatalogItemRepository repository, int id)
        {
            var result = await repository.ItemByIdAsync(id: id);

            if (result == null)
                return BadRequest(result);

            return Ok(result);
        }
    }
}