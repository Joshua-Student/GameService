using GameService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameService.Services
{
    public interface IManager
    {
        public IGame User { get; set; }

        //public ConnectFourUser User;
        public void SetGame(string game);
        public string GetGame();
        public ReturnObject StartGame();

        public ReturnObject TakeTurn(string move);
    }
}
