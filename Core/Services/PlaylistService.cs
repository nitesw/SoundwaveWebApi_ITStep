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
    public class PlaylistService(
        IRepository<Playlist> playlistRepo,
        IRepository<PlaylistTrack> playlistTrackRepo,
        IMapper _mapper,
        IFilesService filesService
            ) : IPlaylistService
    {
        private readonly IRepository<Playlist> playlistRepo = playlistRepo;
        private readonly IRepository<PlaylistTrack> playlistTrackRepo = playlistTrackRepo;
        private readonly IMapper _mapper = _mapper;
        private readonly IFilesService filesService = filesService;

        public async Task Create(CreatePlaylistDto model)
        {
            var entity = _mapper.Map<Playlist>(model);

            entity.ImgUrl = await filesService.SaveFile(model.Image, true);

            await playlistRepo.Insert(entity);
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

            await filesService.DeleteFile(playlist.ImgUrl);

            await playlistRepo.Delete(playlist);
            await playlistRepo.Save();
        }

        public async Task Edit(EditPlaylistDto model)
        {
            var playlist = await playlistRepo.GetById(model.Id);
            if (playlist != null)
            {
                await playlistRepo.Detach(playlist);

                if (model.Image != null)
                {
                    var updatedPlaylist = _mapper.Map<Playlist>(model);
                    updatedPlaylist.ImgUrl = await filesService.EditFile(playlist.ImgUrl, model.Image, true);
                    await playlistRepo.Update(updatedPlaylist);
                }
                else
                {
                    var updatedPlaylist = _mapper.Map<Playlist>(model);
                    updatedPlaylist.ImgUrl = playlist.ImgUrl;
                    await playlistRepo.Update(updatedPlaylist);
                }

                await playlistRepo.Save();
            }
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
