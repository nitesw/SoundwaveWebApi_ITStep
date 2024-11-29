using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Like
    {
        public int Id { get; set; }
        public int TrackId { get; set; }
        public Track? Track { get; set; }
        public string UserId { get; set; }
        public User? User { get; set; }
    }
}
