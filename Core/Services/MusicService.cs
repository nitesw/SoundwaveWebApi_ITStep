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
    public class MusicService(
        IRepository<Track> trackRepo,
        IRepository<PlaylistTrack> playlistTrackRepo,
        IMapper _mapper,
        IFilesService filesService
            ) : IMusicService
    {
        private readonly IRepository<Track> trackRepo = trackRepo;
        private readonly IRepository<PlaylistTrack> playlistTrackRepo = playlistTrackRepo;
        private readonly IMapper _mapper = _mapper;
        private readonly IFilesService filesService = filesService;

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
            var entity = _mapper.Map<Track>(model);

            entity.ImgUrl = await filesService.SaveFile(model.Image, true);
            entity.TrackUrl = await filesService.SaveFile(model.Track, false);

            await trackRepo.Insert(entity);
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
            var track = await trackRepo.GetById(model.Id);
            if (track != null)
            {
                await trackRepo.Detach(track);

                if (model.Image != null)
                {
                    var updatedTrack = _mapper.Map<Track>(model);
                    updatedTrack.ImgUrl = await filesService.EditFile(track.ImgUrl, model.Image, true);
                    if (model.Track != null)
                    {
                        updatedTrack.TrackUrl = await filesService.EditFile(track.TrackUrl, model.Track, false);
                        await trackRepo.Update(updatedTrack);
                    }
                    else
                    {
                        updatedTrack.TrackUrl = track.TrackUrl;
                        await trackRepo.Update(updatedTrack);
                    }
                }
                else
                {
                    var updatedTrack = _mapper.Map<Track>(model);
                    updatedTrack.ImgUrl = track.ImgUrl;
                    if (model.Track != null)
                    {
                        updatedTrack.TrackUrl = await filesService.EditFile(track.TrackUrl, model.Track, false);
                        await trackRepo.Update(updatedTrack);
                    }
                    else
                    {
                        updatedTrack.TrackUrl = track.TrackUrl;
                        await trackRepo.Update(updatedTrack);
                    }
                }
                
                await trackRepo.Save();
            }
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
