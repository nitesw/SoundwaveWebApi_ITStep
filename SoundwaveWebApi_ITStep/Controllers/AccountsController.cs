﻿using Core.Dtos;
using Core.Interfaces;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await accountsService.GetAll());
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
