using LudothekWeb_M133.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace LudothekWeb_M133.Storage
{
    public class GameRepository
    {
        public string DataPath { get; set; }

        public GameRepository(string storagePath)
        {
            DataPath = storagePath;

            // create file and insert data, because there is no way to add via web interface
            if (!File.Exists(DataPath))
            {
                CreateSampleFile();
            }
        }

        public void CreateSampleFile()
        {

            List<Game> games = new List<Game>
            {
                new Game {Id = 1, Name = "Rogue One" },
                new Game {Id = 2, Name = "Game" },
                new Game {Id = 3, Name = "Testgame" },
                new Game {Id = 4, Name = "Crossbow" },
                new Game {Id = 5, Name = "Waterbottle" }
            };

            var json = JsonConvert.SerializeObject(games);
            File.WriteAllText(DataPath, json, Encoding.Unicode);
        }

        public List<Game> GetGames()
        {
            var json = File.ReadAllText(DataPath);
            return JsonConvert.DeserializeObject<List<Game>>(json);
        }

    }
}