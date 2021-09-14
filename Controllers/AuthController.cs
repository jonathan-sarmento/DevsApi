using DevsApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace DevsApi.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ApiBaseController
    {
         IAuthService _service;

        public AuthController (IAuthService service)
        {
            _service = service;
        }

        /// <summary>
        /// Creates a new user for the authentication.
        /// </summary>
        /// <param name="identityUser"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] IdentityUser identityUser)
        {
            IdentityResult result = _service.Create(identityUser).Result;
           
            identityUser.PasswordHash = null;
            return result.Succeeded ? 
                ApiOk(identityUser) :
                ApiBadRequest("Error in creation");
        }

        /// <summary>
        /// Get an access token for an authorized user.
        /// </summary>
        /// <param name="identityUser"></param>
        /// <returns></returns>
        [HttpPost, Route("Token")]
        [AllowAnonymous]
        public IActionResult Token([FromBody] IdentityUser identityUser){
             try 
            {
                return ApiOk(_service.GenerateToken(identityUser));
            }
            catch(Exception e)
            {
                return ApiBadRequest(e, e.Message);
            }
        }
    }
}