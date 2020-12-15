using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiPrincipal.Controllers
{
    //[Authorize]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumoController : ControllerBase
    {
        [HttpGet("getFromOtherApi")]
        public async Task<IActionResult> GetFromOtherApi()
        {
            var tokenGetter = ConsumoGetCredentials.GetInstance();
            await tokenGetter.CheckExpirationToken();
            string token = tokenGetter.Token;
            
            var client = new HttpClient();
            client.SetBearerToken(token);
            var response = await client.GetAsync("https://localhost:5005/api/authorized/test");       
            
            var toReturn = string.Concat("Datos conseguidos desde la api de servicio: ", await response.Content.ReadAsStringAsync());
            return Ok(toReturn);
        }
    }
}
