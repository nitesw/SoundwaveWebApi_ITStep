using Core.Dtos;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ILikeService
    {
        Task<IEnumerable<LikeDto>> GetAll();
        Task<IEnumerable<TrackDto>> GetLikedTracksByUserId(string userId);
        Task<LikeDto> GetLike(int id);
        Task AddLike(CreateLikeDto model);
        Task RemoveLike(int Id);
    }
}
