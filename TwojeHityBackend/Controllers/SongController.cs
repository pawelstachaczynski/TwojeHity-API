using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Runtime.CompilerServices;
using TwojeHity.Models.DTOs;
using TwojeHity.Models.DTOs.User;
using TwojeHity.Services;

namespace TwojeHity.Controllers
{
    [Route("api/song")]
    public class SongController : ControllerBase
    {
        private readonly ISongService _songService;

        public SongController(ISongService songService)
        {
            _songService = songService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SongDto>> GetAllSongs()
        {

            var songs = _songService.GetAllSongs();
            return Ok(songs);
        }

        [HttpPost]
        public async Task<ActionResult> AddNewSong([FromBody] SongDto dto)
        {

            return Ok(await _songService.AddNewSong(dto));
        }

        [HttpPost]
        [Route("new-with-favorite")]
        public async Task<ActionResult> AddNewSongWithFavorite([FromBody] SongDto dto)
        {

            return Ok(await _songService.AddNewSongWithFavorite(dto));
        }

        [HttpPost]
        [Route("add-to-favorite")]
        public async Task<ActionResult> AddToFavorite([FromBody] FavoriteDto dto)
        {
            return Ok(await _songService.AddToFavorite(dto));

        }

        [HttpGet]
        [Route("your-favorite/{userId}")]
        public async Task<ActionResult> GetAllFavorites([FromRoute] int userId)
        {
            return Ok(await _songService.GetAllFavorites(userId));
        }

        [HttpPost]
        [Route("your-favorite-delete")]
        public async Task<ActionResult> DeleteFavorite([FromBody] int songId)
        {
            return Ok(await _songService.DeleteFavorite(songId));
        }
    }
}