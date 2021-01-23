using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Lab_03.Handlers
{
    public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock
            ) : base(options, logger, encoder, clock)
        {

        }

        protected override Task<AuthenticateResult> GetHandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("missing auth header");
            }
            //if header contains this -> basic authentication
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]); //get the "value"
            var credentialsBytes = Convert.FromBase64String(authHeader.Parameter); //convert to arr of bytes
            var credentials = Encoding.UTF8.GetString(credentialsBytes).Split(":"); //from bytes to string in a form of login:password; after split -> [login, password]  


            if (credentials.Length != 2) //each user should have login and password
            {
                return AuthenticateResult.Fail("missing auth header");
            }

            //todo chceck password in db

            //if everything is correct 

            //claims - describe user
            // create identity - represents the user
            //ticket allows user to use specific things

            //regster in configure services method
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
