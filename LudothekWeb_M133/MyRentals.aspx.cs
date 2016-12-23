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

        private string m_currentUser;

        protected void Page_Load(object sender, EventArgs e) {
            m_currentUser = HttpContext.Current.User.Identity.Name;

            var prolongRentalId = Request.QueryString["prolong"];
            var cancelRentalId = Request.QueryString["cancel"];

            if (prolongRentalId != null) {
                // prolong btn clicked
                int id = Int32.Parse(prolongRentalId);
                RentalRepository.ProlongRental(id, m_currentUser);
                Response.Redirect("/MyRentals.aspx");
            } else if (cancelRentalId != null) {
                // cancel btn clicked
                int id = Int32.Parse(cancelRentalId);
                RentalRepository.CancelRental(id, m_currentUser);
                Response.Redirect("/MyRentals.aspx");
            } else {
                // read all rentals of user
                List<Rental> rentals = RentalRepository.GetRentalsForUser(m_currentUser);

                foreach (Rental rental in rentals) {
                    Game gameByRental = GameRepository.ReadGamesFromFile().First(g => g.Id == rental.GameId);

                    // write games to html, sort by availability
                    if (rental.IsActive) {
                        myActiveRentals.InnerHtml += RenderRental(gameByRental, rental, rental.IsActive);
                    } else {
                        myPastRentals.InnerHtml += RenderRental(gameByRental, rental, rental.IsActive);
                    }

                }
            }
        }

        private static string RenderRental(Game game, Rental rental, bool active) {
            if (active) {
                return $"<div class=\"col-md-3 rental\">" +
                       $"<h3>{game.Name}</h3>" +
                       $"<p>From: {rental.StartDate.ToString("D")}</p>" +
                       $"<p>To: {rental.EndDate.ToString("D")}</p>" +
                       $"<a class=\"btn btn-secondary\" href=\"/MyRentals.aspx?cancel={rental.Id}\">Cancel</a>" +
                       $"<a class=\"btn btn-primary\" href=\"/MyRentals.aspx?prolong={rental.Id}\">Prolong</a><hr/>" +
                       "</div>";
            } 
            return $"<div class=\"col-md-3 rental\">" +
                    $"<h3>{game.Name}</h3>" +
                    $"<p>From: {rental.StartDate.ToString("D")}</p>" +
                    $"<p>To: {rental.EndDate.ToString("D")}</p><hr/>" +
                "</div>";
        }


        #endregion
    }
}