using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TwojeHity.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwojeHity.Models;
using TwojeHity.Models.DTOs.User;
using TwojeHity.Settings;
using TwojeHity.Validators;
using TwojeHity.Models.DTOs;
using TwojeHity.Database;
using System.Reflection;
using NLog.Web.LayoutRenderers;
using TwojeHity.Migrations;

namespace TwojeHity.Services
{
    public interface ISongService
    {
        IEnumerable<SongDto> GetAllSongs();
        Task<int> AddNewSong(SongDto song);
        Task<int> AddToFavorite(FavoriteDto dto);
        Task<int> AddNewSongWithFavorite(SongDto dto);
        Task <IEnumerable<SongDto>> GetAllFavorites(int userId);
        public Task<bool> DeleteFavorite(int songId);

    }

    public class SongService : ISongService
{
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ISongRepository _songRepository;

        public SongService(AppDbContext dbContext, IMapper mapper, ILogger<SongService> logger, ISongRepository songRepository)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _songRepository = songRepository;
        }

      public IEnumerable<SongDto> GetAllSongs()
        {
            var songs = _dbContext.Songs.ToList();
            var allsongs = _mapper.Map<List<SongDto>>(songs);
            return allsongs;
        }

        public async Task<int> AddNewSong(SongDto dto)
        {
            var lastRank = await _songRepository.getLastRankNumer();
            dto.Favorites = null;

          
                var song = new Song
                {
                    rank = (int)lastRank + 1,
                    title = dto.title,
                    artist = dto.artist,
                    album = dto.album,
                    year = dto.year,
                };
                int id = await _songRepository.AddNewSong(song);
                return id;

        }


            public async Task<int> AddNewSongWithFavorite(SongDto dto)
            {

                var lastRank = await _songRepository.getLastRankNumer();
                var lastId = await _songRepository.getLastId();
            var Fav = new Favorite
            {
                UserId = dto.Favorites.UserId,
                SongId = lastId
            };
              
                {
                    {
                        var song = new Song
                        {
                            rank = (int)lastRank + 1,
                            title = dto.title,
                            artist = dto.artist,
                            album = dto.album,
                            year = dto.year,
                            Favorites = Fav
                        };
                        int id = await _songRepository.AddNewSong(song);
                        return id;
                    }
                }

            }

        public async Task<int> AddToFavorite(FavoriteDto dto)
        {
            var lastId = await _songRepository.getLastId();
            dto.SongId = lastId;
            var fav = _mapper.Map<Favorite>(dto);
            int id = await _songRepository.AddToFavorite(fav);
            return id;
        }

        public async Task<IEnumerable<SongDto>> GetAllFavorites(int userId)
        {

            var userSongs = await _songRepository.GetAllFavorites(userId);
            var allsongs = _mapper.Map<List<SongDto>>(userSongs);
            return allsongs;
        }

            public async Task<bool> DeleteFavorite(int songId)
        {
            bool id = await _songRepository.DeleteFavorite(songId);
            return id;
        }

    }
}
