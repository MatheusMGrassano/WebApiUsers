using Azure;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApiUser.Models.Requests;
using WebApiUser.Models.Responses;
using WebApiUser.Services;

namespace WebApiUser.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;
        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<string>>> PostAuthenticate(LoginRequest request)
        {
            var response = await _loginService.Authenticate(request);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
