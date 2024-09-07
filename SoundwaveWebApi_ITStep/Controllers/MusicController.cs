using AutoMapper;
using Core.Dtos;
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
        private readonly SoundwaveDbContext _ctx;
        private readonly IMapper _mapper;

        public MusicController(SoundwaveDbContext _ctx, IMapper _mapper)
        {
            this._ctx = _ctx;
            this._mapper = _mapper;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var tracks = _mapper.Map<List<TrackDto>>(
                _ctx.Tracks
                .Include(x => x.Genre)
                .Include(x => x.PlaylistTracks!)
                .ThenInclude(x => x.Playlist)
                .ToList());

            return Ok(tracks);
        }

        [HttpGet("getTrack")]
        public IActionResult Get(int id)
        {
            var track = _ctx.Tracks
                .Include(x => x.PlaylistTracks!)
                .ThenInclude(x => x.Playlist)
                .FirstOrDefault(x => x.Id == id);
            if (track == null) return NotFound();

            _ctx.Entry(track).Reference(x => x.Genre).Load();

            return Ok(_mapper.Map<TrackDto>(track));
        }

        [HttpPost("create")]
        public IActionResult Create(CreateTrackDto model)
        {
            if (!ModelState.IsValid) return BadRequest();

            _ctx.Tracks.Add(_mapper.Map<Track>(model));
            _ctx.SaveChanges();

            return Ok();
        }

        [HttpPut("edit")]
        public IActionResult Edit(EditTrackDto model)
        {
            if (!ModelState.IsValid) return BadRequest();

            _ctx.Tracks.Update(_mapper.Map<Track>(model));
            _ctx.SaveChanges();

            return Ok();
        }
        
        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            var track = _ctx.Tracks.Find(id);
            if (track == null) return NotFound();

            _ctx.Tracks.Remove(track);
            _ctx.SaveChanges();

            return Ok();
        }

        [HttpPatch("archive")]
        public IActionResult Archive(int id)
        {
            var track = _ctx.Tracks.Find(id);
            if (track == null) return NotFound();

            track.IsArchived = true;

            _ctx.Tracks.Update(track);
            _ctx.SaveChanges();

            return Ok();
        }
        [HttpPatch("restore")]
        public IActionResult Restore(int id)
        {
            var track = _ctx.Tracks.Find(id);
            if (track == null) return NotFound();

            track.IsArchived = false;

            _ctx.Tracks.Update(track);
            _ctx.SaveChanges();

            return Ok();
        }

        [HttpPatch("public")]
        public IActionResult Public(int id)
        {
            var track = _ctx.Tracks.Find(id);
            if (track == null) return NotFound();

            track.IsPublic = true;

            _ctx.Tracks.Update(track);
            _ctx.SaveChanges();

            return Ok();
        }
        [HttpPatch("private")]
        public IActionResult Private(int id)
        {
            var track = _ctx.Tracks.Find(id);
            if (track == null) return NotFound();

            track.IsPublic = false;

            _ctx.Tracks.Update(track);
            _ctx.SaveChanges();

            return Ok();
        }
    }
}
