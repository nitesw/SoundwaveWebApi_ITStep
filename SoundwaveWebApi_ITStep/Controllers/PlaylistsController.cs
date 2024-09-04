using Data.Data;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SoundwaveWebApi_ITStep.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly SoundwaveDbContext ctx;

        public PlaylistsController(SoundwaveDbContext ctx)
        {
            this.ctx = ctx;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(ctx.Playlists.ToList());
        }

        [HttpGet("getPlaylist")]
        public IActionResult Get(int id)
        {
            var playlist = ctx.Playlists.Find(id);
            if (playlist == null) return NotFound();

            return Ok(playlist);
        }

        [HttpPost("create")]
        public IActionResult Create(Playlist model)
        {
            if (!ModelState.IsValid) return BadRequest();

            ctx.Playlists.Add(model);
            ctx.SaveChanges();

            return Ok();
        }

        [HttpPut("edit")]
        public IActionResult Edit(Playlist model)
        {
            if (!ModelState.IsValid) return BadRequest();

            ctx.Playlists.Update(model);
            ctx.SaveChanges();

            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            var playlist = ctx.Playlists.Find(id);
            if (playlist == null) return NotFound();

            ctx.Playlists.Remove(playlist);
            ctx.SaveChanges();

            return Ok();
        }
    }
}
