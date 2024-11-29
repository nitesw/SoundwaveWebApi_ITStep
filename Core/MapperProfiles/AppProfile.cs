using AutoMapper;
using Core.Dtos;
using Data.Entities;
using System;
using System.ClientModel.Primitives;
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
            CreateMap<EditTrackDto, Track>();
            CreateMap<Track, TrackDto>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ReverseMap();

            CreateMap<CreatePlaylistDto, Playlist>();
            CreateMap<EditPlaylistDto, Playlist>();
            CreateMap<Playlist, PlaylistDto>()
                .ForMember(x => x.Tracks, opt => opt.MapFrom(src => src.PlaylistTracks.Select(x => x.Track)))
                .ForMember(x => x.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ReverseMap();

            CreateMap<RegisterDto, User>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(src => src.UserName));
            CreateMap<User, UserDto>()
                .ForMember(x => x.TrackCount, opt => opt.MapFrom(src => src.Tracks.Count))
                .ForMember(x => x.PlaylistCount, opt => opt.MapFrom(src => src.Playlists.Count))
                .ReverseMap();

            CreateMap<Genre, GenreDto>().ReverseMap();
        }
    }
}
