using AutoMapper;
using Core.Dtos;
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
        private readonly SoundwaveDbContext _ctx;
        private readonly IMapper _mapper;

        public PlaylistsController(SoundwaveDbContext _ctx, IMapper _mapper)
        {
            this._ctx = _ctx;
            this._mapper = _mapper;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var playlists = _mapper.Map<List<PlaylistDto>>(
                _ctx.Playlists
                .Include(x => x.PlaylistTracks!)
                .ThenInclude(x => x.Track));

            return Ok(playlists);
        }

        [HttpGet("getPlaylist")]
        public IActionResult Get(int id)
        {
            var playlist = _ctx.Playlists
                .Include(x => x.PlaylistTracks!)
                .ThenInclude(x => x.Track)
                .FirstOrDefault(x => x.Id == id);
            if (playlist == null) return NotFound();

            return Ok(_mapper.Map<PlaylistDto>(playlist));
        }

        [HttpPost("create")]
        public IActionResult Create(CreatePlaylistDto model)
        {
            if (!ModelState.IsValid) return BadRequest();

            _ctx.Playlists.Add(_mapper.Map<Playlist>(model));
            _ctx.SaveChanges();

            return Ok();
        }

        [HttpPut("edit")]
        public IActionResult Edit(EditPlaylistDto model)
        {
            if (!ModelState.IsValid) return BadRequest();

            _ctx.Playlists.Update(_mapper.Map<Playlist>(model));
            _ctx.SaveChanges();

            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            var playlist = _ctx.Playlists.Find(id);
            if (playlist == null) return NotFound();

            _ctx.Playlists.Remove(playlist);
            _ctx.SaveChanges();

            return Ok();
        }
    }
}
