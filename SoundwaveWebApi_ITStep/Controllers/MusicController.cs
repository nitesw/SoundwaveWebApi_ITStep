using AutoMapper;
using Core.Dtos;
using Core.Interfaces;
using Data.Data;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SoundwaveWebApi_ITStep.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly IMusicService musicService;

        public MusicController(IMusicService musicService)
        {
            this.musicService = musicService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await musicService.GetAll());
        }

        [HttpGet("getTrack")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await musicService.Get(id));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateTrackDto model)
        {
            await musicService.Create(model);

            return Ok();
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit(EditTrackDto model)
        {
            await musicService.Edit(model);

            return Ok();
        }
        
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await musicService.Delete(id);

            return Ok();
        }

        [HttpPatch("archive")]
        public async Task<IActionResult> Archive(int id)
        {
            await musicService.Archive(id);

            return Ok();
        }
        [HttpPatch("restore")]
        public async Task<IActionResult> Restore(int id)
        {
            await musicService.Restore(id);

            return Ok();
        }

        [HttpPatch("makePublic")]
        public async Task<IActionResult> MakePublic(int id)
        {
            await musicService.MakePublic(id);

            return Ok();
        }
        [HttpPatch("makePrivate")]
        public async Task<IActionResult> MakePrivate(int id)
        {
            await musicService.MakePrivate(id);

            return Ok();
        }
    }
}
