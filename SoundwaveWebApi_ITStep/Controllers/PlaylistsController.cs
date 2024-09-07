using AutoMapper;
using Core.Dtos;
using Core.Interfaces;
using Data.Data;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SoundwaveWebApi_ITStep.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly IPlaylistService playlistService;

        public PlaylistsController(IPlaylistService playlistService)
        {
            this.playlistService = playlistService;
        }

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
        public async Task<IActionResult> Create(CreatePlaylistDto model)
        {
            await playlistService.Create(model);

            return Ok();
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit(EditPlaylistDto model)
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
