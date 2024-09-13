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
    internal class PlaylistSpecification
    {
        internal class ById : Specification<Playlist>
        {
            public ById(int id)
            {
                Query
                    .Where(x => x.Id == id)
                    .Include(x => x.PlaylistTracks!)
                    .ThenInclude(x => x.Track);
            }
        }
        internal class All : Specification<Playlist>
        {
            public All()
            {
                Query
                    .Include(x => x.PlaylistTracks!)
                    .ThenInclude(x => x.Track);
            }
        }
        internal class DeleteById : Specification<Playlist>
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
