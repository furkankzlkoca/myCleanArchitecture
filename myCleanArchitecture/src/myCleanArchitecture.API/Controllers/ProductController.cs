using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myCleanArchitecture.Shared.FeatureModels.Products;
using myCleanArchitecture.Shared.FeatureModels.Products.Commands;
using myCleanArchitecture.Shared.FeatureModels.Products.Queries;
using myCleanArchitecture.Shared.Results;

namespace myCleanArchitecture.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Shared.Results.ObjectResult<ProductDto>>> Get(Guid id)
        {
            return Ok(await _mediator.Send(new GetByIdProductQuery(id)));
        }
        [HttpPost]
        public async Task<ActionResult<PagingResult<ProductDto>>> GetPage([FromBody] GetPageProductQuery model)
        {
            return Ok(await _mediator.Send(model));
        }
        [HttpGet]
        public async Task<ActionResult<ListResult<ProductDto>>> GetAll()
        {
            return Ok(await _mediator.Send(new GetProductsQuery()));
        }
        [HttpPost]
        public async Task<ActionResult<Shared.Results.ObjectResult<ProductDto>>> Create([FromBody] CreateProductCommand model)
        {
            return Ok(await _mediator.Send(model));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteProductCommand(id)));
        }
        [HttpPut]
        public async Task<ActionResult<Shared.Results.ObjectResult<ProductDto>>> Update([FromBody] UpdateProductCommand model)
        {
            return Ok(await _mediator.Send(model));
        }
    }
}
