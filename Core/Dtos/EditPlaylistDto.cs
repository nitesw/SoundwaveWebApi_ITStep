﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class EditPlaylistDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IFormFile? Image { get; set; }
        public string? Description { get; set; }

        public string? UserId { get; set; }
    }
}
