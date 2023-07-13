using AutoMapper;
using TwojeHity.Models;
using TwojeHity.Models.DTOs;
using TwojeHity.Models.DTOs.User;

namespace TwojeHity.Helpers
{

    public class AutoMapperHelper : Profile
    {
        public AutoMapperHelper()
        {
            CreateMap<User, UserDto>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Login, y => y.MapFrom(z => z.Login));


            CreateMap<UserDto, User>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Login, y => y.MapFrom(z => z.Login));

            CreateMap<RegisterUserDto, User>()
                .ForMember(x => x.Login, y => y.MapFrom(z => z.Login));

            CreateMap<User, RegisterUserDto>()
                .ForMember(x => x.Login, y => y.MapFrom(z => z.Login));

            CreateMap<LoginDto, User>()
                .ForMember(x => x.Login, y => y.MapFrom(z => z.Login))
                .ForMember(x => x.PasswordHash, y => y.MapFrom(z => z.Password));

            CreateMap<User, LoginDto>()
               .ForMember(x => x.Login, y => y.MapFrom(z => z.Login))
               .ForMember(x => x.Password, y => y.MapFrom(z => z.PasswordHash));

            CreateMap<Song, SongDto>()
              .ForMember(x => x.rank, y => y.MapFrom(z => z.rank))
              .ForMember(x => x.title, y => y.MapFrom(z => z.title))
              .ForMember(x => x.artist, y => y.MapFrom(z => z.artist))
              .ForMember(x => x.album, y => y.MapFrom(z => z.album))
              .ForMember(x => x.year, y => y.MapFrom(z => z.year));


            CreateMap<SongDto, Song>()
              .ForMember(x => x.rank, y => y.MapFrom(z => z.rank))
              .ForMember(x => x.title, y => y.MapFrom(z => z.title))
              .ForMember(x => x.artist, y => y.MapFrom(z => z.artist))
              .ForMember(x => x.album, y => y.MapFrom(z => z.album))
              .ForMember(x => x.year, y => y.MapFrom(z => z.year));

            CreateMap<FavoriteDto, Favorite>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id));


            CreateMap<Favorite, FavoriteDto>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id));




        }
    }
}