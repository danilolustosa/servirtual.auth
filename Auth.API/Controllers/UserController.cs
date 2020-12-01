using Auth.API.Model;
using Auth.API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Auth.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        UserService _service = new UserService();

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(string email, string password)
        {
            var result = _service.Login(email, password);
            return new ObjectResult(result) { StatusCode = result.StatusCode };
        }

        [HttpGet("list")]
        [Authorize(Roles = "ADM")]
        public IActionResult List()
        {
            var result = _service.List();
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpGet]
        [Authorize(Roles = "ADM,USR")]
        public IActionResult Get([FromQuery] int id)
        {
            var result = _service.Get(id);
            return new ObjectResult(result) { StatusCode = result.StatusCode };
        }

    }
}
