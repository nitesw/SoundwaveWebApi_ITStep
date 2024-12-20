﻿using Microsoft.EntityFrameworkCore;
using Data.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Data.DataSeeder;
using System.Reflection;

namespace Data.Data
{
    public class SoundwaveDbContext : IdentityDbContext<User>
    {
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistTrack> PlaylistTrack { get; set; }
        public DbSet<Like> Likes { get; set; }

        public SoundwaveDbContext() { }
        public SoundwaveDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PlaylistTrack>().HasKey(x => new { x.PlaylistId, x.TrackId });

            modelBuilder.SeedGenres();
            modelBuilder.SeedTracks();

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }  
    }
}
