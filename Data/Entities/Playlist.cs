using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImgUrl { get; set; }
        public string? Description { get; set; }
        // TODO: ability to make playlsit public, maybe add the url to it (the url of itself)

        public ICollection<PlaylistTrack>? PlaylistTracks { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
    }
}
