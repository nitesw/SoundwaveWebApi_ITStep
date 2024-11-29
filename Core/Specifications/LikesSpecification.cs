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
    internal class LikesSpecification
    {
        internal class ById : Specification<Like>
        {
            public ById(int id)
            {
                Query
                    .Where(x => x.Id == id)
                    .Include(x => x.User)
                    .Include(x => x.Track)
                    .ThenInclude(x => x.Genre);
            }
        }
        internal class AllByUserId : Specification<Like>
        {
            public AllByUserId(string userId)
            {
                Query
                    .Where(x => x.UserId == userId)
                    .Include(x => x.Track)
                    .ThenInclude(x => x.Genre)
                    .Include(x => x.User);
            }
        }
        internal class All : Specification<Like>
        {
            public All()
            {
                Query
                    .Include(x => x.Track)
                    .ThenInclude(x => x.Genre)
                    .Include(x => x.User);
            }
        }
        internal class DeleteById : Specification<Like>
        {
            public DeleteById(int id)
            {
                Query
                    .Where(x => x.Id == id)
                    .Include(x => x.User)
                    .Include(x => x.Track);
            }
        }
    }
}
