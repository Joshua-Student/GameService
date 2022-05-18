using GameService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameService.Models
{
    public class ConnectFourUser : IGame
    {
        
        readonly ConnectFourModel model;
        public bool machinePlayer { get; set; }
        public string CurrentMove { get; set; }
        public ConnectFourUser(ConnectFourModel model)
        {
            this.model = model;
        }

        public ReturnObject Move(string move)
        {
            ReturnObject ro = new ReturnObject();
            int col;
            bool illegalMove;


            // Gets the the move from an external source
            //string move = CurrentMove;

            // Checks if move is valid
            illegalMove = !int.TryParse(move, out col) || col < 1 || col > 7;
            if (illegalMove)
            {
                ro.Valid = false;
                ro.Message = "Please pick a number from 1-7...";
                return ro;
            }

            // Checks if selected column is full
            illegalMove = model.CheckIfColumnIsFull(col);
            if (illegalMove)
            {
                ro.Valid = false;
                ro.Message = "That column is full. Please choose again...";
                return ro;
            }

            if (model.IsGameOver())
            {
                ro.Valid = false;
                ro.Message = "Game is over";
                return ro;
            }

            ro.Valid = true;

            // Then puts in the disk and checks if that won the game
            if (model.TakeTurn(col))
            {
                ro.Message = "I won";
                return ro;
            }

            // Then checks if the board is full
            if (model.CheckIfBoardIsFull())
            {
                ro.Message = "It's a draw";
                return ro;
            }

            ro.Message = "Move successful";
            return ro;
        }

        public bool IsMachinePlayer()
        {
            return machinePlayer;
        }

        // Currently gets the move from the console
        //public void SetMove(string move)
        //{
        //    CurrentMove = move;
        //}
    }
}
