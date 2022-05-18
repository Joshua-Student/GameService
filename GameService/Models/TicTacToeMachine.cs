using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameService.Models
{
    public class TicTacToeMachine
    {
        readonly TicTacToeModel model;
        ReturnObject ro;

        public TicTacToeMachine(TicTacToeModel model)
        {
            this.model = model;
        }
        public ReturnObject Move()
        {
            ro = new ReturnObject();

            // Check if you can win
            for (int i = 0; i < 9; i++)
            {
                if (CheckIfCanWin(i))
                {
                    TakeTheTurn(i + 1);
                    ro.OpponentsMove = $"{i + 1}";
                    return ro;
                }
            }

            // Check if you can block a win
            model.SwapPlayer();

            for (int i = 0; i < 9; i++)
            {
                if (CheckIfCanWin(i))
                {
                    model.SwapPlayer();
                    TakeTheTurn(i + 1);
                    ro.OpponentsMove = $"{i + 1}";
                    return ro;
                }
            }
            model.SwapPlayer();

            // Get random move
            int move = GetRandomCol();
            while (model.CheckIfIllegalMove(move))
            {
                move = GetRandomCol();
            }

            // Then takes turn and checks if that won the game
            TakeTheTurn(move);

            // Then checks if the board is full
            if (model.CheckIfDraw())
            {
                Console.WriteLine("It's a draw");
            }

            return ro;
        }

        private void TakeTheTurn(int i)
        {
            if (model.TakeTurn(i))
            {
                ro.Message = "Machine won";
            }
        }

        public bool CheckIfCanWin(int i)
        {
            bool win = false;
            if (!model.CheckIfIllegalMove(i + 1))
            {
                model.board[i] = model.currentPlayer;
                win = model.CheckIfWinner();
                model.board[i] = -1;
            }

            return win;
        }

        public int GetRandomCol()
        {
            Random rand = new Random();
            return rand.Next(1, 10);
        }
    }
}
