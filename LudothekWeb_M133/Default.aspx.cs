// (C) IMT - Information Management Technology AG, CH-9470 Buchs, www.imt.ch.
// SW Guideline: Technote Coding Guidelines Ver. 1.4

using System;
using System.Web;
using LudothekWeb_M133.Models;
using LudothekWeb_M133.Pages;

namespace LudothekWeb_M133 {
    public partial class _Default : SecurePageBase {
        #region Methodes

        protected void Page_Load(object sender, EventArgs e) {
            foreach (Game game in GameRepository.GetGames()) {
                gamesList.InnerHtml += "<div><h3>" + game.Name + "</h3><a href=\"#\" OnClick=\"CreateRental("+game.Id+")\">Rent</a></div>";
            }
        }

        private void CreateRental(int gameId) {
            if (RentalRepository.IsGameAvailable(gameId)) {
                string username = HttpContext.Current.User.Identity.Name;
                RentalRepository.CreateRental(gameId, username);
            }
        }

        #endregion
    }
}