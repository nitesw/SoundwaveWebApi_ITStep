using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPlaylistService
    {
        Task<IEnumerable<PlaylistDto>> GetAll();
        Task<PlaylistDto> Get(int id);
        Task Create(CreatePlaylistDto model);
        Task Edit(EditPlaylistDto model);
        Task Delete(int id);
    }
}
