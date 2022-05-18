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
        bool Machine;
        public ConnectFourUser user;
        public ConnectFourMachine machine;

        //public ConnectFourUser User { get; set; }

        //IGame IManager.User { get; set; }

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

                if(Machine)
                {
                    machine = new ConnectFourMachine(model);
                }

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

        public void SetGame(string game, bool machine)
        {
            Game = game;
            Machine = machine;

        }

        //public string GetGame()
        //{
        //    return Game;
        //}

        public ReturnObject TakeTurn(string move)
        {
            ReturnObject ro = new ReturnObject();
            ro = user.Move(move);

            if (!ro.Valid)
            {
                return ro;
            }

            if (Machine)
            {
                ReturnObject temp = machine.Move();
                ro.OpponentsMove = temp.OpponentsMove;

                if (temp.Message is not null)
                    ro.Message = temp.Message;
            }

            return ro;
        }
    }
}
