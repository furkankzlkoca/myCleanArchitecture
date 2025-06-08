using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myCleanArchitecture.Shared.FeatureModels.Authentication;
using myCleanArchitecture.Shared.FeatureModels.Authentication.Commands;

namespace myCleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("register")]
        public async Task<ActionResult<AuthModel>> Register(RegisterUserCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthModel>> Login(LoginUserCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthModel>> RefreshToken(RefreshTokenCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(LogoutUserCommand request)
        {
            await _mediator.Send(request);
            return NoContent();
        }
    }
}
