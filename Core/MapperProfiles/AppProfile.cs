using AutoMapper;
using Core.Dtos;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.MapperProfiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<CreateTrackDto, Track>().ForMember(x => x.UploadDate, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<EditTrackDto, Track>().ForMember(x => x.UploadDate, opt => opt.Ignore());
            CreateMap<TrackDto, Track>().ReverseMap();
        }
    }
}
