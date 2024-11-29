using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class LikeDto
    {
        public int Id { get; set; }
        public int TrackId { get; set; }
        public string UserId { get; set; }
    }
}
