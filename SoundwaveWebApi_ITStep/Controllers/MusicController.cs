using AutoMapper;
using Core.Dtos;
using Core.Interfaces;
using Core.Models;
using Data.Data;
using Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SoundwaveWebApi_ITStep.Extensions;
using System;
using System.Collections.Generic;

namespace SoundwaveWebApi_ITStep.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MusicController : ControllerBase
    {
        private readonly IMusicService musicService;

        public MusicController(IMusicService musicService)
        {
            this.musicService = musicService;
        }

        [AllowAnonymous]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await musicService.GetAll());
        }

        [AllowAnonymous]
        [HttpGet("genres")]
        public async Task<IActionResult> GetGenres()
        {
            return Ok(await musicService.GetGenres());
        }

        [AllowAnonymous]
        [HttpGet("getTrack")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await musicService.Get(id));
        }

        [Authorize(Roles = $"{Roles.ADMIN},{Roles.PROUSER}", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CreateTrackDto model)
        {
            await musicService.Create(model);

            return Ok();
        }

        [Authorize(Roles = $"{Roles.ADMIN},{Roles.PROUSER}", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromForm] EditTrackDto model)
        {
            await musicService.Edit(model);

            return Ok();
        }

        [Authorize(Roles = $"{Roles.ADMIN},{Roles.PROUSER}", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await musicService.Delete(id);

            return Ok();
        }

        [Authorize(Roles = Roles.ADMIN, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPatch("archive")]
        public async Task<IActionResult> Archive(int id)
        {
            await musicService.Archive(id);

            return Ok();
        }
        [Authorize(Roles = Roles.ADMIN, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPatch("restore")]
        public async Task<IActionResult> Restore(int id)
        {
            await musicService.Restore(id);

            return Ok();
        }
    }
}
