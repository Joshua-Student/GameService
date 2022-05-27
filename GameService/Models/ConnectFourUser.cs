using GameService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameService.Models
{
    public class ConnectFourUser
    {

        readonly ConnectFourModel model;
        ConnectFourMachine machine;
        public bool MachinePlayer { get; set; }
        public ConnectFourUser(ConnectFourModel model, bool machineOn)
        {
            this.model = model;

            if (machineOn)
            {
                machine = new ConnectFourMachine(model);
                MachinePlayer = true;
            }
        }

        public ReturnObject Move(string move)
        {
            ReturnObject ro = new ReturnObject();
            int col;
            bool illegalMove;

            // Checks if game is in session
            if (model.IsGameOver())
            {
                ro.Valid = false;
                ro.Message = "Game is over";
                return ro;
            }

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

        public ReturnObject MachineTurn()
        {
            return machine.Move();
        }

        public bool HasMachinePlayer()
        {
            return MachinePlayer;
        }

    }
}
