using LudothekWeb_M133.Models;
using LudothekWeb_M133.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LudothekWeb_M133 {
    public partial class _Default : Page {
        protected void Page_Load(object sender, EventArgs e)
        {

            GameRepository gameRepository = new GameRepository(Server.MapPath("Storage/GameData.json"));

            foreach (Game game in gameRepository.GetGames())
            {
                gamesList.InnerHtml += "<h1>" + game.Name + "</h1>";
            }
        }
    }
}