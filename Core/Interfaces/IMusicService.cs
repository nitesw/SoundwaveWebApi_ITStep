using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMusicService
    {
        Task<IEnumerable<TrackDto>> GetAll();
        Task<IEnumerable<GenreDto>> GetGenres();
        Task<TrackDto> Get(int id);
        Task Create(CreateTrackDto model);
        Task Edit(EditTrackDto model);
        Task Delete(int id);
        Task Archive(int id);
        Task Restore(int id);
    }
}
