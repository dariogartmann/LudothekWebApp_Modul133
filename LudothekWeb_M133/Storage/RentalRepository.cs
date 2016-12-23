// (C) IMT - Information Management Technology AG, CH-9470 Buchs, www.imt.ch.
// SW Guideline: Technote Coding Guidelines Ver. 1.4

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LudothekWeb_M133.Models;
using Newtonsoft.Json;

namespace LudothekWeb_M133.Storage {
    public class RentalRepository {
        #region Properties

        public string DataPath { get; set; }

        #endregion

        #region Constructor

        public RentalRepository(string storagePath) {
            DataPath = storagePath;

            // create file and insert data, because there is no way to add via web interface
            if (!File.Exists(DataPath)) {
                InitializeFile();
            }
        }

        #endregion

        #region Methodes

        public void InitializeFile() {
            List<Rental> rentals = new List<Rental>();
            var json = JsonConvert.SerializeObject(rentals);
            File.WriteAllText(DataPath, json, Encoding.Unicode);
        }

        public void CreateRental(int gameId, string username) {
            // read rentals from file
            var json = File.ReadAllText(DataPath);
            List<Rental> allRentals = JsonConvert.DeserializeObject<List<Rental>>(json).ToList();

            // create new rental with data
            Rental newRental = new Rental {
                Id = allRentals.Count + 1,
                User = username,
                GameId = gameId,
                IsActive = true,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(7)
            };
            allRentals.Add(newRental);

            // save to json
            File.WriteAllText(DataPath, JsonConvert.SerializeObject(allRentals), Encoding.Unicode);
        }

        public List<Rental> GetAllRentals() {
            var json = File.ReadAllText(DataPath);
            return JsonConvert.DeserializeObject<List<Rental>>(json);
        }

        public List<Rental> GetRentalsForUser(string username) {
            return GetAllRentals().Where(r => r.User == username).ToList();
        }

        public bool IsGameAvailable(int gameId) {
            return !(GetAllRentals().Count(r => r.GameId == gameId) > 0);
        }

        #endregion
    }
}