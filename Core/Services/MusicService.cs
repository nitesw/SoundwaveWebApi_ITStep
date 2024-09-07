using AutoMapper;
using Core.Dtos;
using Core.Interfaces;
using Data.Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class MusicService : IMusicService
    {
        private readonly SoundwaveDbContext _ctx;
        private readonly IMapper _mapper;

        public MusicService(SoundwaveDbContext _ctx, IMapper _mapper)
        {
            this._ctx = _ctx;
            this._mapper = _mapper;
        }

        public async Task Archive(int id)
        {
            var track = await _ctx.Tracks.FindAsync(id);
            if (track == null) return;

            track.IsArchived = true;
            await _ctx.SaveChangesAsync();
        }

        public async Task Create(CreateTrackDto model)
        {
            _ctx.Tracks.Add(_mapper.Map<Track>(model));
            await _ctx.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var track = await _ctx.Tracks.FindAsync(id);
            if (track == null) return;

            _ctx.Tracks.Remove(track);
            await _ctx.SaveChangesAsync();
        }

        public async Task Edit(EditTrackDto model)
        {
            _ctx.Tracks.Update(_mapper.Map<Track>(model));
            await _ctx.SaveChangesAsync();
        }

        public async Task<TrackDto> Get(int id)
        {
            var track = await _ctx.Tracks
                .Include(x => x.PlaylistTracks!)
                .ThenInclude(x => x.Playlist)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (track == null) return null;

            await _ctx.Entry(track).Reference(x => x.Genre).LoadAsync();

            return _mapper.Map<TrackDto>(track);
        }

        public async Task<IEnumerable<TrackDto>> GetAll()
        {
            var tracks = await _ctx.Tracks
                .Include(x => x.Genre)
                .Include(x => x.PlaylistTracks!)
                .ThenInclude(x => x.Playlist)
                .ToListAsync();

            return _mapper.Map<List<TrackDto>>(tracks);
        }

        public async Task MakePrivate(int id)
        {
            var track = await _ctx.Tracks.FindAsync(id);
            if (track == null) return;

            track.IsPublic = false;
            await _ctx.SaveChangesAsync();
        }

        public async Task MakePublic(int id)
        {
            var track = await _ctx.Tracks.FindAsync(id);
            if (track == null) return;

            track.IsPublic = true;
            await _ctx.SaveChangesAsync();
        }

        public async Task Restore(int id)
        {
            var track = await _ctx.Tracks.FindAsync(id);
            if (track == null) return;

            track.IsArchived = false;
            await _ctx.SaveChangesAsync();
        }
    }
}
