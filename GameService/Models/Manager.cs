using GameService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameService.Models
{
    public class Manager : IManager
    {
        public ConnectFourUser C_user;
        public ConnectFourMachine C_machine;
        public TicTacToeUser T_user;
        public TicTacToeMachine T_machine;
        public WordleUser W_user;
        public WordleMachine W_machine;

        public Manager()
        {
        }

        public ReturnObject StartGame(string game, bool machine)
        {
            //string Game = game;
            //bool Machine = machine;
            ReturnObject ro = new ReturnObject();
            if (game == "ConnectFour")
            {
                ConnectFourModel model = new ConnectFourModel();
                C_user = new ConnectFourUser(model);

                if (machine)
                {
                    C_user.MachinePlayer = true;
                    C_machine = new ConnectFourMachine(model);
                }

                ro.Valid = true;
                ro.Message = "Game started";
                return ro;
            }
            else if (game == "TicTacToe")
            {
                TicTacToeModel model = new TicTacToeModel();
                T_user = new TicTacToeUser(model);

                if (machine)
                {
                    T_user.MachinePlayer = true;
                    T_machine = new TicTacToeMachine(model);
                }

                ro.Valid = true;
                ro.Message = "Game started";
                return ro;
            }
            if (game == "Wordle")
            {
                WordleModel model = new WordleModel();
                W_user = new WordleUser(model);

                if (machine)
                {
                    W_user.MachinePlayer = true;
                    W_machine = new WordleMachine(model);
                    ro = W_machine.Move();
                    return ro;
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

        public ReturnObject TakeTurn(string game, string move)
        {
            ReturnObject ro = new ReturnObject();
            switch (game)
            {
                case "ConnectFour":
                    ro = ConnectFourTurn(move);
                    break;
                case "TicTacToe":
                    ro = TicTacToeTurn(move);
                    break;
                case "Wordle":
                    ro = WordleTurn(move);
                    break;
                default:
                    ro.Valid = false;
                    ro.Message = "Invalid Game";
                    break;
            }

            return ro;
        }

        private ReturnObject WordleTurn(string move)
        {
            ReturnObject ro = new ReturnObject();
            ro = W_user.Move(move);

            if (!ro.Valid)
            {
                return ro;
            }

            if (W_user.MachinePlayer)
            {
                ReturnObject temp = W_machine.Move();
                ro.OpponentsMove = temp.OpponentsMove;

                if (temp.Message is not null)
                    ro.Message = temp.Message;
            }

            return ro;
        }

        private ReturnObject ConnectFourTurn(string move)
        {
            ReturnObject ro = new ReturnObject();
            ro = C_user.Move(move);

            if (!ro.Valid)
            {
                return ro;
            }

            if (C_user.MachinePlayer)
            {
                ReturnObject temp = C_machine.Move();
                ro.OpponentsMove = temp.OpponentsMove;

                if (temp.Message is not null)
                    ro.Message = temp.Message;
            }

            return ro;
        }

        private ReturnObject TicTacToeTurn(string move)
        {
            ReturnObject ro = new ReturnObject();
            ro = T_user.Move(move);

            if (!ro.Valid)
            {
                return ro;
            }

            if (T_user.MachinePlayer)
            {
                ReturnObject temp = T_machine.Move();
                ro.OpponentsMove = temp.OpponentsMove;

                if (temp.Message is not null)
                    ro.Message = temp.Message;
            }

            return ro;
        }
    }
}
