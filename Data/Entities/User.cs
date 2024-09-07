using Microsoft.AspNetCore.Identity;

namespace Data.Entities
{
    public class User : IdentityUser
    {
        public ICollection<Track>? Tracks { get; set; }
        public ICollection<Playlist>? Playlists { get; set; }
    }
}
