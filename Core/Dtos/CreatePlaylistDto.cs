using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class CreatePlaylistDto
    {
        public string Title { get; set; }
        public string ImgUrl { get; set; }
        public string? Description { get; set; }

        public string? UserId { get; set; }
    }
}
