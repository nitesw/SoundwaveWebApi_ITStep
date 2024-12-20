﻿using Ardalis.Specification;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    internal class UserSpecification
    {
        internal class All : Specification<User>
        {
            public All()
            {
                Query
                    .Include(x => x.Tracks)
                    .Include(x => x.Playlists)
                    .Include(x => x.Likes!)
                    .ThenInclude(x => x.Track);
            }
        }
        internal class GetUser : Specification<User>
        {
            public GetUser(string id)
            {
                Query
                    .Where(x => x.Id == id)
                    .Include(x => x.Tracks)
                    .Include(x => x.Playlists)
                    .Include(x => x.Likes!)
                    .ThenInclude(x => x.Track);
            }
        }
    }
}