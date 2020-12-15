using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiPrincipal.Controllers
{

    class ConsumoGetCredentials
    {
        private static readonly ConsumoGetCredentials _instanciaConsumo = new ConsumoGetCredentials();
        public ConsumoGetCredentials()
        {
        }
        public static ConsumoGetCredentials GetInstance() => _instanciaConsumo;

        public string Token { get; set; }
        private DateTime FechaExpiracionToken {get;set;}
        public string GetTokenValue() => Token;
        public async Task CheckExpirationToken()
        {
            if(FechaExpiracionToken == null || DateTime.Now >= FechaExpiracionToken)
            {
                await GetToken();
            }
        }
        public async Task GetToken()
        {
            var client = new HttpClient();

            var tokenRequest = new ClientCredentialsTokenRequest
            {
                Address = "https://localhost:5001/connect/token",
                ClientId = "Api_Principal",
                ClientSecret = "1234567890asdfghjkl1234567890asdfghjkl",
                Scope = "ApiServicio"
            };
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(tokenRequest);

            if (tokenResponse.IsError)
            {
                throw new Exception("Error obteniendo el token de acceso a la api de servicio, revisar la peticion");
            }
            else
            {
                Token = tokenResponse.AccessToken;
                FechaExpiracionToken = DateTime.Now.AddSeconds(3540);
            }            
        }        
    }
}
