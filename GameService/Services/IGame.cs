using GameService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameService.Services
{
    public interface IGame
    {
        public ReturnObject Move(string move);
        //public void SetMove(string move);
    }
}
