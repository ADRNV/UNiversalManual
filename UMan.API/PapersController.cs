using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UMan.Core;
using UMan.Core.Pagination;
using UMan.Domain.Papers;

namespace UMan.API
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
        [HttpGet("papers/{queryParameters}")]
        public async Task<Paper[]> Papers([FromQuery] QueryParameters queryParameters)
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
        [HttpGet("papers/{id}")]
        public async Task<Paper> Papers([FromQuery] int id) => await _mediator.Send(new Get.CommandById(id));
    }
}
