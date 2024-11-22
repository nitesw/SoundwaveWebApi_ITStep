using AutoMapper;
using Core.Dtos;
using Core.Interfaces;
using Data.Data;
using Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoundwaveWebApi_ITStep.Extensions;

namespace SoundwaveWebApi_ITStep.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PlaylistsController : ControllerBase
    {
        private readonly IPlaylistService playlistService;

        public PlaylistsController(IPlaylistService playlistService)
        {
            this.playlistService = playlistService;
        }

        [Authorize(Roles = Roles.ADMIN, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await playlistService.GetAll());
        }

        [HttpGet("getPlaylist")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await playlistService.Get(id));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CreatePlaylistDto model)
        {
            await playlistService.Create(model);

            return Ok();
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromForm] EditPlaylistDto model)
        {
            await playlistService.Edit(model);

            return Ok();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await playlistService.Delete(id);

            return Ok();
        }
    }
}
