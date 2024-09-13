﻿using AutoMapper;
using Core.Dtos;
using Core.Exceptions;
using Core.Interfaces;
using Core.Specifications;
using Data.Data;
using Data.Entities;
using Data.Repositories;
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
        private readonly IRepository<Playlist> playlistRepo;
        private readonly IRepository<PlaylistTrack> playlistTrackRepo;
        private readonly IMapper _mapper;

        public PlaylistService(
            IRepository<Playlist> playlistRepo,
            IRepository<PlaylistTrack> playlistTrackRepo,
            IMapper _mapper
            )
        {
            this.playlistRepo = playlistRepo;
            this.playlistTrackRepo = playlistTrackRepo;
            this._mapper = _mapper;
        }

        public async Task Create(CreatePlaylistDto model)
        {
            await playlistRepo.Insert(_mapper.Map<Playlist>(model));
            await playlistRepo.Save();
        }

        public async Task Delete(int id)
        {
            var playlist = await playlistRepo.GetItemBySpec(new PlaylistSpecification.DeleteById(id));
            if (playlist == null) throw new HttpException(
                $"Playlist with id {id} not found.",
                HttpStatusCode.NotFound);

            if (playlist.PlaylistTracks != null)
                await playlistTrackRepo.RemoveRange(playlist.PlaylistTracks);

            await playlistRepo.Delete(playlist);
            await playlistRepo.Save();
        }

        public async Task Edit(EditPlaylistDto model)
        {
            await playlistRepo.Update(_mapper.Map<Playlist>(model));
            await playlistRepo.Save();
        }

        public async Task<PlaylistDto> Get(int id)
        {
            var playlist = await playlistRepo.GetItemBySpec(new PlaylistSpecification.ById(id));
            if (playlist == null) throw new HttpException(
                $"Playlist with id {id} not found.",
                HttpStatusCode.NotFound);

            return _mapper.Map<PlaylistDto>(playlist);
        }

        public async Task<IEnumerable<PlaylistDto>> GetAll()
        {
            var playlists = await playlistRepo.GetListBySpec(new PlaylistSpecification.All());

            return _mapper.Map<List<PlaylistDto>>(playlists);
        }
    }
}
