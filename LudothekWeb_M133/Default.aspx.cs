// (C) IMT - Information Management Technology AG, CH-9470 Buchs, www.imt.ch.
// SW Guideline: Technote Coding Guidelines Ver. 1.4

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using LudothekWeb_M133.Models;
using LudothekWeb_M133.Pages;

namespace LudothekWeb_M133 {
    public partial class _Default : SecurePageBase {
        #region Methodes

        protected void Page_Load(object sender, EventArgs e) {

            var gameId = Request.QueryString["gameId"];

            if (gameId != null) {
                var selectedGame = GameRepository.ReadGamesFromFile().First(g => g.Id.ToString() == gameId);

                if (selectedGame == null) {
                    return;
                }

                if (RentalRepository.IsGameAvailable(selectedGame.Id)) {
                    string username = HttpContext.Current.User.Identity.Name;
                    RentalRepository.CreateRental(selectedGame.Id, username);
                    Response.Redirect("~/MyRentals.aspx");
                }

            } else {
                foreach (Game game in GameRepository.ReadGamesFromFile()) {
                    if (RentalRepository.IsGameAvailable(game.Id)) {
                        gamesList.InnerHtml += RenderGame(game);
                    }
                }
            }
        }

        private static string RenderGame(Game game) {
            return "<div class=\"col-md-3 game\">" +
                        $"<h3>{game.Name}</h3>" +
                        $"<p>Price: CHF {game.Price}</p>" +
                        $"<a class=\"btn btn-primary\" href=\"/Default.aspx?gameId={game.Id}\">Rent game!</a><hr/>" +
                    "</div>";
        }

        #endregion
    }
}