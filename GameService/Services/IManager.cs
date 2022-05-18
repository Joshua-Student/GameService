using GameService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameService.Services
{
    public interface IManager
    {
        //public IGame User { get; set; }

        //public ConnectFourUser User;
        //public void SetGame(string game, bool machine);
        //public string GetGame();
        public ReturnObject StartGame(string game, bool machine);

        public ReturnObject TakeTurn(string game, string move);
    }
}
