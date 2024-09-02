using Microsoft.AspNetCore.Identity;

namespace Data.Entities
{
    public class User : IdentityUser
    {
        /*public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Likes { get; set; }
        public int Playlists { get; set; }
        public bool IsAdmin { get; set; }
        public int Comments { get; set; }
        public int Follows { get; set; }
        public bool IsPro { get; set ;}*/

        public ICollection<Track>? Tracks { get; set; }
        public ICollection<Playlist>? Playlists { get; set; }
    }
}
