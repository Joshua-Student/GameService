using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameService.Models
{
    public class TicTacToeModel
    {
        public int[] board;
        public readonly int boardSize = 9;
        public bool gameInSession = true;
        public int currentPlayer = 1;

        public TicTacToeModel()
        {
            board = new int[boardSize];
            for (int i = 0; i < boardSize; i++)
            {
                board[i] = -1;
            }
        }

        public bool TakeTurn(int move)
        {
            move--;

            board[move] = currentPlayer;

            if (CheckIfWinner())
            {
                gameInSession = false;
                return true;
            }

            SwapPlayer();

            return false;
        }

        public bool CheckIfWinner()
        {
            return (board[0] == board[1] && board[0] == board[2] && board[0] != -1) ||
                (board[0] == board[3] && board[0] == board[6] && board[0] != -1) ||
                (board[0] == board[4] && board[0] == board[8] && board[0] != -1) ||
                (board[1] == board[4] && board[1] == board[7] && board[1] != -1) ||
                (board[2] == board[5] && board[2] == board[8] && board[2] != -1) ||
                (board[2] == board[4] && board[2] == board[6] && board[2] != -1) ||
                (board[3] == board[4] && board[3] == board[5] && board[3] != -1) ||
                (board[6] == board[7] && board[6] == board[8] && board[6] != -1);
        }

        public bool CheckIfDraw()
        {
            for (int i = 0; i < boardSize; i++)
            {
                if (board[i] == -1)
                    return false;
            }

            gameInSession = false;
            return true;
        }

        public void SwapPlayer()
        {
            currentPlayer = currentPlayer == 1 ? 2 : 1;
        }

        public bool CheckIfIllegalMove(int move)
        {
            return board[move - 1] != -1;
        }

        public bool IsGameOver()
        {
            return !gameInSession;
        }
    }
}
