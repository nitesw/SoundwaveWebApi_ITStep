using Ardalis.Specification;
using AutoMapper;
using Core.Dtos;
using Core.Exceptions;
using Core.Interfaces;
using Core.Specifications;
using Data.Entities;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class LikeService(IRepository<Like> likeRepo, IMapper _mapper) : ILikeService
    {
        private readonly IRepository<Like> likeRepo = likeRepo;
        private readonly IMapper _mapper = _mapper;

        public async Task AddLike(CreateLikeDto model)
        {
            var entity = _mapper.Map<Like>(model);

            await likeRepo.Insert(entity);
            await likeRepo.Save();
        }

        public async Task<IEnumerable<LikeDto>> GetAll()
        {
            var likes = await likeRepo.GetListBySpec(new LikesSpecification.All());

            return _mapper.Map<IEnumerable<LikeDto>>(likes);
        }

        public async Task<LikeDto> GetLike(int id)
        {
            var like = await likeRepo.GetItemBySpec(new LikesSpecification.ById(id));
            if (like == null) throw new HttpException(
                $"Like with id {id} not found.",
                HttpStatusCode.NotFound);

            return _mapper.Map<LikeDto>(like);
        }

        public async Task<IEnumerable<TrackDto>> GetLikedTracksByUserId(string userId)
        {
            var likes = await likeRepo.GetListBySpec(new LikesSpecification.AllByUserId(userId));

            if (likes == null || !likes.Any())
            {
                return new List<TrackDto>();
            }

            var trackDtos = likes
                .Select(like => _mapper.Map<TrackDto>(like.Track))
                .ToList();

            return trackDtos;
        }

        public async Task RemoveLike(int id)
        {
            var like = await likeRepo.GetItemBySpec(new LikesSpecification.DeleteById(id));
            if (like == null) throw new HttpException(
                $"Like with id {id} not found.",
                HttpStatusCode.NotFound);

            await likeRepo.Delete(like);
            await likeRepo.Save();
        }
    }
}
