// (C) IMT - Information Management Technology AG, CH-9470 Buchs, www.imt.ch.
// SW Guideline: Technote Coding Guidelines Ver. 1.4

using System.Web;
using System.Web.UI;
using LudothekWeb_M133.Storage;

namespace LudothekWeb_M133.Pages {
    public class SecurePageBase : Page {
        protected GameRepository GameRepository { get; }
        protected RentalRepository RentalRepository { get; }

        public SecurePageBase() {
            // redirect user to login if not authenticated
            if (!HttpContext.Current.User.Identity.IsAuthenticated) {
                HttpContext.Current.Response.Redirect("/Account/Login.aspx");
            }

            GameRepository = new GameRepository(Server.MapPath("Storage/games.json"));
            RentalRepository = new RentalRepository(Server.MapPath("Storage/rentals.json"));
        }
    }
}