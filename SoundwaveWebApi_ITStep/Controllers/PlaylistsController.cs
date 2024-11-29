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

        [Authorize(Roles = $"{Roles.ADMIN},{Roles.PROUSER},{Roles.USER}", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await playlistService.GetAll());
        }

        [Authorize(Roles = $"{Roles.ADMIN},{Roles.PROUSER},{Roles.USER}", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("getPlaylist")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await playlistService.Get(id));
        }

        [Authorize(Roles = $"{Roles.ADMIN},{Roles.PROUSER},{Roles.USER}", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CreatePlaylistDto model)
        {
            await playlistService.Create(model);

            return Ok();
        }

        [Authorize(Roles = $"{Roles.ADMIN},{Roles.PROUSER},{Roles.USER}", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromForm] EditPlaylistDto model)
        {
            await playlistService.Edit(model);

            return Ok();
        }

        [Authorize(Roles = $"{Roles.ADMIN},{Roles.PROUSER},{Roles.USER}", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await playlistService.Delete(id);

            return Ok();
        }

        [Authorize(Roles = $"{Roles.ADMIN},{Roles.PROUSER},{Roles.USER}", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("addTrack")]
        public async Task<IActionResult> AddTrack(int playlistId, int trackId)
        {
            await playlistService.AddTrackToPlaylist(playlistId, trackId);

            return Ok();
        }
        [Authorize(Roles = $"{Roles.ADMIN},{Roles.PROUSER},{Roles.USER}", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("deleteTrack")]
        public async Task<IActionResult> RemoveTrack(int playlistId, int trackId)
        {
            await playlistService.RemoveTrackFromPlaylist(playlistId, trackId);

            return Ok();
        }
    }
}
