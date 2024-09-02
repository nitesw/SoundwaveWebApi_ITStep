using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class PlaylistTrack
    {
        public int PlaylistId { get; set; }
        public Playlist? Playlist { get; set; }
        public int TrackId { get; set; }
        public Track? Track { get; set; }
    }
}
