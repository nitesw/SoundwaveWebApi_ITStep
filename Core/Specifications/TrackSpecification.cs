using Ardalis.Specification;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Core.Specifications
{
    internal class TrackSpecification
    {
        internal class ById : Specification<Track>
        {
            public ById(int id)
            {
                Query
                    .Where(x => x.Id == id)
                    .Include(x => x.PlaylistTracks!)
                    .ThenInclude(x => x.Playlist)
                    .Include(x => x.Genre);
            }
        }
        internal class All : Specification<Track>
        {
            public All()
            {
                Query
                    .Include(x => x.PlaylistTracks!)
                    .ThenInclude(x => x.Playlist)
                    .Include(x => x.User)
                    .Include(x => x.Genre);
            }
        }
        internal class DeleteById : Specification<Track>
        {
            public DeleteById(int id)
            {
                Query
                    .Where(x => x.Id == id)
                    .Include(x => x.PlaylistTracks);
            }
        }
    }
}
