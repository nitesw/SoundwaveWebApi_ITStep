using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class TrackDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string TrackUrl { get; set; }
        public string ImgUrl { get; set; }
        public bool IsPublic { get; set; }
        public bool IsArchived { get; set; }
        public string? AdditionalTags { get; set; }
        public DateTime UploadDate { get; set; }
        public string? ArtistName { get; set; }

        public int GenreId { get; set; }
        public string? GenreName { get; set; }
        public string? UserId { get; set; }
        public string? UserUsername { get; set; }
        public ICollection<PlaylistDto>? Playlists { get; set; }
    }
}
