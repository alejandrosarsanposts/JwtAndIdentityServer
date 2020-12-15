using ApiPrincipal.Models;
using ApiPrincipal.Services;
using Microsoft.AspNetCore.Mvc;


namespace ApiPrincipal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            this._loginService = loginService;
        }

        [HttpPost("HacerLogin")]
        public IActionResult HacerLogin([FromBody] LoginModel model)
        {
            var response = _loginService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Error de autenticacion, usuario o contraseña incorrectos" });

            return Ok(response);
        }

        
    }
}
