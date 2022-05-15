using GameService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameService.Models
{
    public class Manager : IManager
    {
        string Game;
        public ConnectFourUser user;

        //public ConnectFourUser User { get; set; }

        IGame IManager.User { get; set; }

        public Manager()
        {
            Game = "";
        }

        public ReturnObject StartGame()
        {
            ReturnObject ro = new ReturnObject();
            if (Game == "ConnectFour")
            {
                ConnectFourModel model = new ConnectFourModel();
                user = new ConnectFourUser(model);

                ro.Valid = true;
                ro.Message = "Game started";
                return ro;
            }
            else
            {
                ro.Valid = false;
                ro.Message = "Invalid Game";
                return ro;
            }
        }

        public void SetGame(string game)
        {
            Game = game;
        }

        public string GetGame()
        {
            return Game;
        }

        public ReturnObject TakeTurn(string move)
        {
            return user.Move(move);
        }
    }
}
