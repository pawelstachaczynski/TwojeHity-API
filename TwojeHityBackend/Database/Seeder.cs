
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.IO;
using System.Net.Http.Json;
using TwojeHity.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TwojeHity.Database
{

    public class Seeder
    {
        private readonly AppDbContext _dbContext;
        public Seeder(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (!_dbContext.Database.CanConnect())
            {
                _dbContext.Database.Migrate();
            }

            if (_dbContext.Database.CanConnect())
            {
               if(!_dbContext.Songs.Any()) {


                    string jsonContent = File.ReadAllText("songs.json");
                    var jsonData = JObject.Parse(jsonContent);
                    var songArray = jsonData["songs"].ToObject<JArray>();

                    List<Song> songList = songArray.ToObject<List<Song>>();
                    _dbContext.Songs.AddRange(songList);
                    _dbContext.SaveChanges();
                }
            }
            }
        }

    }
