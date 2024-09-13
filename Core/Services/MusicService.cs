using AutoMapper;
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
    public class MusicService : IMusicService
    {
        private readonly IRepository<Track> trackRepo;
        private readonly IRepository<PlaylistTrack> playlistTrackRepo;
        private readonly IMapper _mapper;

        public MusicService(
            IRepository<Track> trackRepo, 
            IRepository<PlaylistTrack> playlistTrackRepo, 
            IMapper _mapper
            )
        {
            this.trackRepo = trackRepo;
            this.playlistTrackRepo = playlistTrackRepo;
            this._mapper = _mapper;
        }

        public async Task Archive(int id)
        {
            var track = await trackRepo.GetById(id);
            if (track == null) throw new HttpException(
                $"Track with id {id} not found.",
                HttpStatusCode.NotFound);

            track.IsArchived = true;
            await trackRepo.Save();
        }

        public async Task Create(CreateTrackDto model)
        {
            await trackRepo.Insert(_mapper.Map<Track>(model));
            await trackRepo.Save();
        }

        public async Task Delete(int id)
        {
            var track = await trackRepo.GetItemBySpec(new TrackSpecification.DeleteById(id));
            if (track == null) throw new HttpException(
                $"Track with id {id} not found.",
                HttpStatusCode.NotFound);

            if (track.PlaylistTracks != null)
                await playlistTrackRepo.RemoveRange(track.PlaylistTracks);
            await trackRepo.Delete(track);
            await trackRepo.Save();
        }

        public async Task Edit(EditTrackDto model)
        {
            await trackRepo.Update(_mapper.Map<Track>(model));
            await trackRepo.Save();
        }

        public async Task<TrackDto> Get(int id)
        {
            var track = await trackRepo.GetItemBySpec(new TrackSpecification.ById(id));
            if (track == null) throw new HttpException(
                $"Track with id {id} not found.",
                HttpStatusCode.NotFound);

            return _mapper.Map<TrackDto>(track);
        }

        public async Task<IEnumerable<TrackDto>> GetAll()
        {
            var tracks = await trackRepo.GetListBySpec(new TrackSpecification.All());

            return _mapper.Map<List<TrackDto>>(tracks);
        }

        public async Task MakePrivate(int id)
        {
            var track = await trackRepo.GetById(id);
            if (track == null) throw new HttpException(
                $"Track with id {id} not found.",
                HttpStatusCode.NotFound);

            track.IsPublic = false;
            await trackRepo.Save();
        }

        public async Task MakePublic(int id)
        {
            var track = await trackRepo.GetById(id);
            if (track == null) throw new HttpException(
                $"Track with id {id} not found.",
                HttpStatusCode.NotFound);

            track.IsPublic = true;
            await trackRepo.Save();
        }

        public async Task Restore(int id)
        {
            var track = await trackRepo.GetById(id);
            if (track == null) throw new HttpException(
                $"Track with id {id} not found.",
                HttpStatusCode.NotFound);

            track.IsArchived = false;
            await trackRepo.Save();
        }
    }
}
