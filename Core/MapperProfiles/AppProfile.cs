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
            CreateMap<Track, TrackDto>()
                .ForMember(x => x.Playlists, opt => opt.MapFrom(src => src.PlaylistTracks.Select(x => x.Playlist)))
                .ReverseMap();

            CreateMap<CreatePlaylistDto, Playlist>();
            CreateMap<EditPlaylistDto, Playlist>();
            CreateMap<Playlist, PlaylistDto>()
                .ForMember(x => x.Tracks, opt => opt.MapFrom(src => src.PlaylistTracks.Select(x => x.Track)))
                .ReverseMap();
        }
    }
}
