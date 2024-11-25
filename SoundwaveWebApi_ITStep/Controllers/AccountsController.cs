using Core.Dtos;
using Core.Interfaces;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoundwaveWebApi_ITStep.Extensions;

namespace SoundwaveWebApi_ITStep.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService accountsService;

        public AccountsController(IAccountsService accountsService)
        {
            this.accountsService = accountsService;
        }

        [Authorize(Roles = Roles.ADMIN, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await accountsService.GetAll());
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("getUser")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await accountsService.Get(id));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            await accountsService.Register(model);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            return Ok(await accountsService.Login(model));
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            return Ok();
        }
    }
}
