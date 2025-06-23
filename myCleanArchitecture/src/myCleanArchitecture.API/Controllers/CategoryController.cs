using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myCleanArchitecture.Shared.FeatureModels.Categories;
using myCleanArchitecture.Shared.FeatureModels.Categories.Commands;
using myCleanArchitecture.Shared.FeatureModels.Categories.Queries;
using myCleanArchitecture.Shared.Results;

namespace myCleanArchitecture.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Shared.Results.ObjectResult<CategoryDto>>> Get(Guid id)
        {
            return Ok(await _mediator.Send(new GetByIdCategoryQuery(id)));
        }
        [HttpGet]
        public async Task<ActionResult<ListResult<CategoryDto>>> GetAll()
        {
            return Ok(await _mediator.Send(new GetCategoriesQuery()));
        }
        [HttpPost]
        public async Task<ActionResult<PagingResult<CategoryDto>>> GetPage([FromBody] GetPageCategoryQuery model)
        {
            return Ok(await _mediator.Send(model));
        }
        [HttpPost]
        public async Task<ActionResult<Shared.Results.ObjectResult<CategoryDto>>> Create([FromBody] CreateCategoryCommand model)
        {
            return Ok(await _mediator.Send(model));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteCategoryCommand(id)));
        }
        [HttpPut]
        public async Task<ActionResult<Shared.Results.ObjectResult<CategoryDto>>> Update([FromBody] UpdateCategoryCommand model)
        {
            return Ok(await _mediator.Send(model));
        }
    }
}
