using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using WebApiUser.Models;
using WebApiUser.Models.Requests;
using WebApiUser.Models.Responses;
using WebApiUser.Services.UserService;

namespace WebApiUser.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<List<UserResponse>>>> Get([FromQuery] int offset, int limit = 10)
        {
            return Ok(await _userService.GetUserList(offset, limit));
        }

        [HttpGet("Id/{id}")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<UserResponse>>> GetById(int id)
        {
            var response = await _userService.GetUserById(id);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("Email/{email}")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<UserResponse>>> GetById(string email)
        {
            var response = await _userService.GetUserByEmail(email);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<ServiceResponse<UserResponse>>> Post(CreateUserRequest request)
        {
            var response = await _userService.CreateUser(request);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("UpdateName")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<User>>> UpdateName(UpdateUserNameRequest request)
        {
            var response = await _userService.UpdateNameUser(request);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("UpdateEmail")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<User>>> UpdateEmail(UpdateUserEmailRequest request)
        {
            var response = await _userService.UpdateEmailUser(request);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("UpdatePassword")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<User>>> UpdatePassword(UpdateUserPasswordRequest request)
        {
            var response = await _userService.UpdatePasswordUser(request);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<User>>> Delete(int id)
        {
            var response = await _userService.DeleteUser(id);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
