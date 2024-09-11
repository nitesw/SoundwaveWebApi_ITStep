using AutoMapper;
using Core.Dtos;
using Core.Exceptions;
using Core.Interfaces;
using Data.Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly SoundwaveDbContext _ctx;
        private readonly IMapper _mapper;

        public PlaylistService(SoundwaveDbContext _ctx, IMapper _mapper)
        {
            this._ctx = _ctx;
            this._mapper = _mapper;
        }

        public async Task Create(CreatePlaylistDto model)
        {
            _ctx.Playlists.Add(_mapper.Map<Playlist>(model));
            await _ctx.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var playlist = await _ctx.Playlists.FindAsync(id);
            if (playlist == null) throw new HttpException(
                $"Playlist with id {id} not found.",
                HttpStatusCode.NotFound);

            _ctx.Playlists.Remove(playlist);
            await _ctx.SaveChangesAsync();
        }

        public async Task Edit(EditPlaylistDto model)
        {
            _ctx.Playlists.Update(_mapper.Map<Playlist>(model));
            await _ctx.SaveChangesAsync();
        }

        public async Task<PlaylistDto> Get(int id)
        {
            var playlist = await _ctx.Playlists
                .Include(x => x.PlaylistTracks!)
                .ThenInclude(x => x.Track)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (playlist == null) throw new HttpException(
                $"Playlist with id {id} not found.",
                HttpStatusCode.NotFound);

            return _mapper.Map<PlaylistDto>(playlist);
        }

        public async Task<IEnumerable<PlaylistDto>> GetAll()
        {
            var playlists = await _ctx.Playlists
                .Include(x => x.PlaylistTracks!)
                .ThenInclude(x => x.Track)
                .ToListAsync();

            return _mapper.Map<List<PlaylistDto>>(playlists);
        }
    }
}
