using GameService.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameService.Models
{
    public class Manager : IManager
    {
        Guid guid;
        Dictionary<Guid, ConnectFourUser> CFGames;
        Dictionary<Guid, TicTacToeUser> TTTGames;
        Dictionary<Guid, WordleUser> WGames;



        public Manager()
        {
            CFGames = new Dictionary<Guid, ConnectFourUser>();
            TTTGames = new Dictionary<Guid, TicTacToeUser>();
            WGames = new Dictionary<Guid, WordleUser>();
        }

        public ReturnObject StartGame(string game, bool machine)
        {
            ReturnObject ro = new ReturnObject();
            if (game == "ConnectFour")
            {
                var model = new ConnectFourModel();
                var connectFourUser = new ConnectFourUser(model, machine);
                guid = Guid.NewGuid();
                CFGames.Add(guid, connectFourUser);

                ro.Valid = true;
                ro.Message = $"{guid}";
                return ro;
            }
            else if (game == "TicTacToe")
            {
                var model = new TicTacToeModel();
                var ticTacToeUser = new TicTacToeUser(model, machine);
                guid = Guid.NewGuid();
                TTTGames.Add(guid, ticTacToeUser);

                ro.Valid = true;
                ro.Message = $"{guid}";
                return ro;
            }
            if (game == "Wordle")
            {
                var model = new WordleModel();
                
                if (machine)
                {
                    var wordleMachine = new WordleMachine(model);
                    ro = wordleMachine.Move();
                    return ro;
                }

                var wordleUser = new WordleUser(model);
                guid = Guid.NewGuid();
                WGames.Add(guid, wordleUser);

                ro.Valid = true;
                ro.Message = $"{guid}";
                return ro;
            }
            else
            {
                ro.Valid = false;
                ro.Message = "Invalid Game";
                return ro;
            }
        }

        public ReturnObject TakeTurn(string game, Guid guid, string move)
        {
            ReturnObject ro = new ReturnObject();

            switch (game)
            {
                case "ConnectFour":
                    ro = ConnectFourTurn(guid, move);
                    break;
                case "TicTacToe":
                    ro = TicTacToeTurn(guid, move);
                    break;
                case "Wordle":
                    ro = WordleTurn(guid, move);
                    break;
                default:
                    ro.Valid = false;
                    ro.Message = "Invalid Game";
                    break;
            }

            return ro;
        }

        private ReturnObject WordleTurn(Guid guid, string move)
        {
            ReturnObject ro = new ReturnObject();

            if (!WGames.ContainsKey(guid))
            {
                ro.Valid = false;
                ro.Message = "Guid not valid";
                return ro;
            }

            ro = WGames[guid].Move(move);

            return ro;
        }

        private ReturnObject ConnectFourTurn(Guid guid, string move)
        {
            ReturnObject ro = new ReturnObject();

            if (!CFGames.ContainsKey(guid))
            {
                ro.Valid = false;
                ro.Message = "Guid not valid";
                return ro;
            }

            ro = CFGames[guid].Move(move);

            if (!ro.Valid)
            {
                return ro;
            }

            if (CFGames[guid].HasMachinePlayer())
            {
                ReturnObject temp = CFGames[guid].MachineTurn();
                ro.OpponentsMove = temp.OpponentsMove;

                if (temp.Message is not null)
                    ro.Message = temp.Message;
            }

            return ro;
        }

        private ReturnObject TicTacToeTurn(Guid guid, string move)
        {
            ReturnObject ro = new ReturnObject();
            if (!TTTGames.ContainsKey(guid))
            {
                ro.Valid = false;
                ro.Message = "Guid not valid";
                return ro;
            }

            ro = TTTGames[guid].Move(move);

            if (!ro.Valid)
            {
                return ro;
            }

            if (TTTGames[guid].HasMachinePlayer())
            {
                ReturnObject temp = TTTGames[guid].MachineTurn();
                ro.OpponentsMove = temp.OpponentsMove;

                if (temp.Message is not null)
                    ro.Message = temp.Message;
            }

            return ro;
        }

    }
}
