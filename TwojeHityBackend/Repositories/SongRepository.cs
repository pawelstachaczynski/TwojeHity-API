using TwojeHity.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;
using TwojeHity.Models;
using TwojeHity.Models.DTOs;
using TwojeHity.Migrations;

namespace TwojeHity.Repositories
{

    public interface ISongRepository
    {
        public Task<int> getLastRankNumer();
        public Task<int> getLastId();
        public Task<int> AddNewSong(Song dto);
        public Task<int> AddToFavorite(Favorite fav);
        public Task<IEnumerable<Song>> GetAllFavorites(int userId);

        public Task<bool> DeleteFavorite(int song);
    }
    public class SongRepository : ISongRepository
    {
        private readonly AppDbContext _context;

        public SongRepository(AppDbContext context)
        {
            _context = context;
            
        }

        public async Task<IEnumerable<Song>> GetAllFavorites(int userId)
        {
            var wynik = from fav in _context.Favorites
                        join song in _context.Songs on fav.SongId equals song.Id
                        where fav.UserId == userId
                        select song;

            return await wynik.ToListAsync();
        }

        public async Task<int> getLastRankNumer()
        {
            return await _context.Songs.MaxAsync(x => x.rank);
            
        }

        public async Task<int> getLastId()
        {
            return await _context.Songs.MaxAsync(x => x.Id);

        }

        public async Task<int> AddNewSong(Song dto)
        {
            _context.Songs.Add(dto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex.InnerException; 
            }

            return dto.Id;
        }

        public async Task<int> AddToFavorite(Favorite fav)
        {
            _context.Favorites.Add(fav);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return fav.Id;
        }

        public async Task<bool> DeleteFavorite(int song)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var favoritesToDelete = _context.Favorites.Where(fav => fav.SongId == song);
                    _context.Favorites.RemoveRange(favoritesToDelete);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return false;
                }
            }
            return true;
        }


    }
}
