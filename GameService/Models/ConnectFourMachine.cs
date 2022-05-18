using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameService.Models
{
    public class ConnectFourMachine
    {
        ConnectFourModel model;
        ReturnObject ro;

        public ConnectFourMachine(ConnectFourModel model)
        {
            this.model = model;
        }

        public ReturnObject Move()
        {
            ro = new ReturnObject();

            // Check if you can win
            for (int i = 0; i < 7; i++)
            {
                if (model.CheckIfColumnIsFull(i + 1))
                    continue;

                // Enter disk to see if that will win
                model.EnterDiskIntoBoard(i);

                if (GreedyAlgo(i))
                {
                    model.RemoveDiskFromBoard(i);
                    TakeTheTurn(i + 1);
                    ro.OpponentsMove = $"{i + 1}";
                    return ro;
                }
                model.RemoveDiskFromBoard(i);
            }

            // Check if you can block a win
            model.SwapColor(ref model.currentColor);

            for (int i = 0; i < 7; i++)
            {
                if (model.CheckIfColumnIsFull(i + 1))
                    continue;

                model.EnterDiskIntoBoard(i);

                if (GreedyAlgo(i))
                {
                    model.RemoveDiskFromBoard(i);
                    model.SwapColor(ref model.currentColor);
                    TakeTheTurn(i + 1);

                    ro.OpponentsMove = $"{i + 1}";
                    return ro;
                }
                model.RemoveDiskFromBoard(i);
            }

            model.SwapColor(ref model.currentColor);

            // Get random move
            int col = GetRandomCol();
            while (model.CheckIfColumnIsFull(col))
            {
                col = GetRandomCol();
            }

            ro.OpponentsMove = $"{col}";
            // Put in the disk and check if that won the game
            TakeTheTurn(col);

            //if (model.TakeTurn(col))
            //{
            //    ro.Message = "Machine won";
            //    return ro;
            //}

            // Then check if the board is full
            if (model.CheckIfBoardIsFull())
            {
                ro.Message = "It's a draw";
            }

            return ro;
        }

        public void TakeTheTurn(int i)
        {
            if (model.TakeTurn(i))
            {
                ro.Message = "Machine won";
            }
        }

        public int GetRandomCol()
        {
            Random rand = new Random();
            return rand.Next(1, 8);
        }

        public bool GreedyAlgo(int col)
        {
            int row = model.height - model.columns[col];
            return model.CheckIfWinner(row, col);
        }
    }
}
