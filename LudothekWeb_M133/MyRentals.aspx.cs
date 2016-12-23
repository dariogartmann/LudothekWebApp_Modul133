// (C) IMT - Information Management Technology AG, CH-9470 Buchs, www.imt.ch.
// SW Guideline: Technote Coding Guidelines Ver. 1.4

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LudothekWeb_M133.Models;
using LudothekWeb_M133.Pages;

namespace LudothekWeb_M133 {
    public partial class MyRentals : SecurePageBase {
        #region Methodes

        protected void Page_Load(object sender, EventArgs e) {
            List<Rental> rentals = RentalRepository.GetRentalsForUser(HttpContext.Current.User.Identity.Name);

            foreach (Rental rental in rentals) {
                Game gameByRental = GameRepository.GetGames().First(g => g.Id == rental.GameId);

                myRentals.InnerHtml += gameByRental;
            }

        }

        #endregion
    }
}