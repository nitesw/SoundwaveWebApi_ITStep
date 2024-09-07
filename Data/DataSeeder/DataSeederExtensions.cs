using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataSeeder
{
    public static class DataSeederExtensions
    {
        public static void SeedGenres(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData(new List<Genre>()
            {
                new Genre() { Id = 1, Name = "None" },
                new Genre() { Id = 2, Name = "Alternative Rock" },
                new Genre() { Id = 3, Name = "Ambient" },
                new Genre() { Id = 4, Name = "Classical" },
                new Genre() { Id = 5, Name = "Country" },
                new Genre() { Id = 6, Name = "Dance & EDM" },
                new Genre() { Id = 7, Name = "Dancehall" },
                new Genre() { Id = 8, Name = "Deep House" },
                new Genre() { Id = 9, Name = "Disco" },
                new Genre() { Id = 10, Name = "Drum & Bass" },
                new Genre() { Id = 11, Name = "Dubstep" },
                new Genre() { Id = 12, Name = "Electronic" },
                new Genre() { Id = 13, Name = "Folk & Singer-Songwriter" },
                new Genre() { Id = 14, Name = "Hip-hop & Rap" },
                new Genre() { Id = 15, Name = "House" },
                new Genre() { Id = 16, Name = "Indie" },
                new Genre() { Id = 17, Name = "Jazz & Blues" },
                new Genre() { Id = 18, Name = "Latin" },
                new Genre() { Id = 19, Name = "Metal" },
                new Genre() { Id = 20, Name = "Piano" },
                new Genre() { Id = 21, Name = "Pop" },
                new Genre() { Id = 22, Name = "R&B & Soul" },
                new Genre() { Id = 23, Name = "Reggae" },
                new Genre() { Id = 24, Name = "Reggaeton" },
                new Genre() { Id = 25, Name = "Rock" },
                new Genre() { Id = 26, Name = "Soundtrack" },
                new Genre() { Id = 27, Name = "Techno" },
                new Genre() { Id = 28, Name = "Trance" },
                new Genre() { Id = 29, Name = "Trap" },
                new Genre() { Id = 30, Name = "Triphop" },
                new Genre() { Id = 31, Name = "World" },
                new Genre() { Id = 32, Name = "Other" }
            });
        }public static void SeedTracks(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Track>().HasData(new List<Track>()
            {
                new Track() { Id = 1, Title = "True Track", TrackUrl = "www.google.com", ImgUrl = "www.google.com", IsPublic = true, IsArchived = false, UploadDate = DateTime.Now, GenreId = 1 },
                new Track() { Id = 2, Title = "True Track 2", TrackUrl = "www.google.com", ImgUrl = "www.google.com", IsPublic = true, IsArchived = false, UploadDate = DateTime.Now, GenreId = 3 }
            });
        }
    }
}
