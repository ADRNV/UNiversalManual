using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UMan.Core;
using UMan.Core.Pagination;
using UMan.Domain.Papers;

namespace UMan.API.Features.Papers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PapersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PapersController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// Gets page of Papers
        /// </summary>
        /// <param name="queryParameters">Parameters for pagination</param>
        /// <returns>Page of parameters</returns>
        [HttpGet("page/{queryParameters}")]
        public async Task<Paper[]> Get([FromQuery] QueryParameters queryParameters)
        {
            var page = await _mediator.Send(new Get.CommandByQueryParameters(queryParameters));

            HttpContext.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(page));

            return page.Items.ToArray();
        }

        /// <summary>
        /// Gets concrect Paper
        /// </summary>
        /// <param name="id">id of Paper</param>
        /// <returns>Page of parameters</returns>
        [HttpGet]
        public async Task<Paper> Get([FromQuery] int id) => await _mediator.Send(new Get.CommandById(id));

        /// <summary>
        /// Creates new <see cref="Paper"/>
        /// </summary>
        /// <param name="newPaper">New <see cref="Paper"/></param>
        /// <returns>Id of new <see cref="Paper"/></returns>
        [HttpPost("create/")]
        public async Task<int> Create([FromQuery] Paper newPaper) => await _mediator.Send(new Create.Command(newPaper));

        /// <summary>
        /// Updates exist <see cref="Paper"/>
        /// </summary>
        /// <param name="oldPaper">Existing paper</param>
        /// <param name="updatePaper">New paper</param>
        /// <returns>Id of updated <see cref="Paper"/></returns>
        [HttpPut("papers/edit")]
        public async Task<int> Update([FromQuery] int oldPaper, [FromBody] Paper updatePaper) =>
            await _mediator.Send(new Update.Command(updatePaper, oldPaper));

        /// <summary>
        /// Delete exist <see cref="Paper"/>
        /// </summary>
        /// <param name="id">Id of exists <see cref="Paper"/></param>
        /// <returns><see langword="true"/> - if deleted, else - <see langword="false"/></returns>
        [HttpDelete("papers/delete")]
        public async Task<bool> Delete([FromQuery] int id) => await _mediator.Send(new Delete.Command(id));
    }
}
