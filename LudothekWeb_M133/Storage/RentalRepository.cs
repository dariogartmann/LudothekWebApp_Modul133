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

        private List<Rental> AllRentals { get; }

        #endregion

        #region Constructor

        public RentalRepository(string storagePath) {
            DataPath = storagePath;

            // create file and insert data, because there is no way to add via web interface
            if (!File.Exists(DataPath)) {
                InitializeFile();
            }
            AllRentals = ReadRentalsFromFile();
        }

        #endregion

        #region Methodes

        public void InitializeFile() {
            List<Rental> rentals = new List<Rental>();
            var json = JsonConvert.SerializeObject(rentals);
            File.WriteAllText(DataPath, json, Encoding.Unicode);
        }

        public void CreateRental(int gameId, string username) {
            // create new rental with data and add to local storage
            Rental newRental = new Rental {
                Id = AllRentals.Count + 1,
                User = username,
                GameId = gameId,
                IsActive = true,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(7)
            };
            AllRentals.Add(newRental);

            // save to json
            SaveRentalData();
        }

        public List<Rental> ReadRentalsFromFile() {
            var json = File.ReadAllText(DataPath);
            return JsonConvert.DeserializeObject<List<Rental>>(json);
        }

        public List<Rental> GetRentalsForUser(string username) {
            return AllRentals.Where(r => r.User == username).ToList();
        }

        /// <summary>
        /// check if a game is available
        /// </summary>
        /// <param name="gameId">id of game to check</param>
        /// <returns>true if available, otherwise false</returns>
        public bool IsGameAvailable(int gameId) {
            return !(AllRentals.Count(r => r.GameId == gameId && r.IsActive) > 0);
        }

        public void ProlongRental(int rentalId, string username) {
            Rental rental = AllRentals.Single(r => r.Id == rentalId && r.User == username && r.IsActive);

            // user can only prolong his rental
            if (rental == null) {
                throw new UnauthorizedAccessException();
            }
            rental.StartDate = DateTime.Today;
            rental.EndDate = DateTime.Today.AddDays(7);
            SaveRentalData();
        }

        public void CancelRental(int rentalId, string username) {
            Rental rental = AllRentals.Single(r => r.Id == rentalId && r.User == username);

            // user can only cancel his rental
            if (rental == null) {
                throw new UnauthorizedAccessException();
            }
            rental.IsActive = false;
            SaveRentalData();
        }

        /// <summary>
        /// save local allRentals to JSON
        /// </summary>
        private void SaveRentalData() {
            File.WriteAllText(DataPath, JsonConvert.SerializeObject(AllRentals), Encoding.Unicode);
        }

        #endregion
    }
}