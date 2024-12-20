﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class CreateTrackDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public IFormFile? Track { get; set; }
        public IFormFile? Image { get; set; }
        public bool IsPublic { get; set; }
        public bool IsArchived { get; set; }
        public string? AdditionalTags { get; set; }
        public string? ArtistName { get; set; }

        public int GenreId { get; set; }
        public string? UserId { get; set; }
    }
}
