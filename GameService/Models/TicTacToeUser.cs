using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameService.Models
{
    public class TicTacToeUser
    {
        readonly TicTacToeModel model;
        public bool MachinePlayer { get; set; }
        public TicTacToeUser(TicTacToeModel model)
        {
            this.model = model;
        }

        public ReturnObject Move(string move)
        {
            ReturnObject ro = new ReturnObject();
            int spot;

            // Checks if game is in session
            if (model.IsGameOver())
            {
                ro.Valid = false;
                ro.Message = "Game is over";
                return ro;
            }

            // Checks if input is legal
            bool illegalMove = !int.TryParse(move, out spot) || spot < 1 || spot > 9;
            if (illegalMove)
            {
                ro.Valid = false;
                ro.Message = "Please pick a number from 1-9...";
                return ro;
            }

            // Check if spot is open
            illegalMove = model.CheckIfIllegalMove(spot);
            if (illegalMove)
            {
                ro.Valid = false;
                ro.Message = "That spot is taken. Please choose again...";
                return ro;
            }

            ro.Valid = true;

            // Then takes turn and checks if that won the game
            if (model.TakeTurn(spot))
            {
                ro.Message = "I won";
                return ro;
            }

            // Then checks if the board is full
            if (model.CheckIfDraw())
            {
                ro.Message = "It's a draw";
                return ro;
            }

            ro.Message = "Move successful";
            return ro;

        }

    }
}
