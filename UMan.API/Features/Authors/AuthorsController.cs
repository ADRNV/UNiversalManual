using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UMan.Core;
using UMan.Core.Pagination;

namespace UMan.API.Features.Authors
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AuthorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets page of Authors
        /// </summary>
        /// <param name="queryParameters">Parameters for pagination</param>
        /// <returns>Page of <see cref="Author"/></returns>
        [HttpGet("/page/{queryParameters}")]
        [AllowAnonymous]
        public async Task<Page<Author>> Get([FromQuery] QueryParameters queryParameters) =>
            await _mediator.Send(new Get.CommandByQueryParameters(queryParameters));


        /// <summary>
        /// Gets concrect Author
        /// </summary>
        /// <param name="id">id of Author</param>
        /// <returns>Concrect <see cref="Author"/></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<Author> Get([FromQuery] int id) => await _mediator.Send(new Get.CommandById(id));

        /// <summary>
        /// Creates new <see cref="Author"/>
        /// </summary>
        /// <param name="newAuthor">New <see cref="Author"/></param>
        /// <returns>Id of new <see cref="Paper"/></returns>
        [HttpPost("create/")]
        public async Task<int> Create([FromBody] Author newAuthor) => await _mediator.Send(new Create.Command(newAuthor));

        /// <summary>
        /// Updates exist <see cref="Author"/>
        /// </summary>
        /// <param name="oldAuthor">Existing author</param>
        /// <param name="newAuthor">New author</param>
        /// <returns>Id of updated <see cref="Author"/></returns>
        [HttpPut("edit")]
        public async Task<int> Update([FromBody] Author newAuthor, [FromQuery] int oldAuthor) =>
            await _mediator.Send(new Update.Command(newAuthor, oldAuthor));

        /// <summary>
        /// Delete exist <see cref="Author"/>
        /// </summary>
        /// <param name="id">Id of exists <see cref="Author"/></param>
        /// <returns><see langword="true"/> - if deleted, else - <see langword="false"/></returns>
        [HttpDelete("delete")]
        public async Task<bool> Delete([FromQuery] int id) => await _mediator.Send(new Delete.Command(id));
    }
}
