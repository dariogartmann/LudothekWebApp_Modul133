// (C) IMT - Information Management Technology AG, CH-9470 Buchs, www.imt.ch.
// SW Guideline: Technote Coding Guidelines Ver. 1.4

using System.Collections.Generic;
using System.IO;
using System.Text;
using LudothekWeb_M133.Models;
using Newtonsoft.Json;

namespace LudothekWeb_M133.Storage {
    public class GameRepository {
        #region Properties

        public string DataPath { get; set; }

        #endregion

        #region Constructor

        public GameRepository(string storagePath) {
            DataPath = storagePath;

            // create file and insert data, because there is no way to add via web interface
            if (!File.Exists(DataPath)) {
                CreateSampleFile();
            }
        }

        #endregion

        #region Methodes

        public void CreateSampleFile() {
            List<Game> games = new List<Game> {
                new Game {Id = 1, Name = "Rogue One", Price = 20.50},
                new Game {Id = 2, Name = "Game", Price = 108.90},
                new Game {Id = 3, Name = "Papier", Price = 9.90},
                new Game {Id = 4, Name = "Cards Against Humanity", Price = 31.50},
                new Game {Id = 5, Name = "Uno", Price = 5.30}
            };
            var json = JsonConvert.SerializeObject(games);
            File.WriteAllText(DataPath, json, Encoding.Unicode);
        }

        public List<Game> GetGames() {
            var json = File.ReadAllText(DataPath);
            return JsonConvert.DeserializeObject<List<Game>>(json);
        }

        #endregion
    }
}