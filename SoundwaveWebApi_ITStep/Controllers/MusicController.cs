using Data.Data;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SoundwaveWebApi_ITStep.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly SoundwaveDbContext ctx;

        public MusicController(SoundwaveDbContext ctx)
        {
            this.ctx = ctx;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(ctx.Tracks.ToList());
        }

        [HttpGet("getTrack")]
        public IActionResult Get(int id)
        {
            var track = ctx.Tracks.Find(id);
            if (track == null) return NotFound();

            return Ok(track);
        }

        [HttpPost("create")]
        public IActionResult Create(Track model)
        {
            if (!ModelState.IsValid) return BadRequest();

            ctx.Tracks.Add(model);
            ctx.SaveChanges();

            return Ok();
        }

        [HttpPut("edit")]
        public IActionResult Edit(Track model)
        {
            if (!ModelState.IsValid) return BadRequest();

            ctx.Tracks.Update(model);
            ctx.SaveChanges();

            return Ok();
        }
        
        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            var track = ctx.Tracks.Find(id);
            if (track == null) return NotFound();

            ctx.Tracks.Remove(track);
            ctx.SaveChanges();

            return Ok();
        }

        [HttpPut("archive")]
        public IActionResult Archive(int id)
        {
            var track = ctx.Tracks.Find(id);
            if (track == null) return NotFound();

            track.IsArchived = true;

            ctx.Tracks.Update(track);
            ctx.SaveChanges();

            return Ok();
        }
        [HttpPut("unarchive")]
        public IActionResult UnArchive(int id)
        {
            var track = ctx.Tracks.Find(id);
            if (track == null) return NotFound();

            track.IsArchived = false;

            ctx.Tracks.Update(track);
            ctx.SaveChanges();

            return Ok();
        }
    }
}
