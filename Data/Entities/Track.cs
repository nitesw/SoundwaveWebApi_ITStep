namespace Data.Entities
{
    public class Track
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string TrackUrl { get; set; }
        public string ImgUrl { get; set; }
        public bool IsPublic { get; set; }
        public bool? IsArchived { get; set; }
        public string? AdditionalTags { get; set; }
        public DateTime UploadDate { get; set; }
        public string? ArtistName { get; set; }

        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }

        public ICollection<PlaylistTrack>? PlaylistTracks { get; set; }
    }
}
