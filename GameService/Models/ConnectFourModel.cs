using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Models
{
    public enum Color
    {
        RED,
        YELLOW,
        NONE
    }
    public class Spot
    {
        public Color Color { get; set; }

        public Spot(Color color)
        {
            this.Color = color;
        }

        public override string ToString()
        {
            if (Color == Color.RED)
                return "X";
            else if (Color == Color.YELLOW)
                return "O";
            return " ";
        }

    }

    public class ConnectFourModel
    {
        public Spot[,] board;
        public readonly int height = 6, width = 7;
        public int[] columns;
        public bool gameInSession = true;
        public Color currentColor = Color.YELLOW;

        public ConnectFourModel()
        {
            InstantiateModel();
        }

        private void InstantiateModel()
        {
            // Create and fill board
            board = new Spot[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    board[i, j] = new Spot(Color.NONE);
                }
            }

            // Create array to hold how many disks are in each column
            columns = new int[width];

        }

        // Takes turn by entering disk into board and checking if the game is won
        public bool TakeTurn(int col)
        {
            col--;

            EnterDiskIntoBoard(col);

            if (CheckIfWinner(height - columns[col], col))
            {
                gameInSession = false;
                return true;
            }
            else
            {
                SwapColor(ref currentColor);
                return false;
            }
        }

        // Enters disk into board by changing the color of chosen spot to currentColor
        public void EnterDiskIntoBoard(int col)
        {
            // Uses the height of the board minus how many disks are already that column to find the row
            board[height - 1 - columns[col], col].Color = currentColor;
            columns[col]++;
        }

        public void RemoveDiskFromBoard(int col)
        {
            // Uses the height of the board minus how many disks are already that column to find the row
            columns[col]--;
            board[height - 1 - columns[col], col].Color = Color.NONE;
        }

        public bool CheckIfColumnIsFull(int col)
        {
            return columns[col - 1] == height;
        }

        // Calls CheckDirection for all four directions
        public bool CheckIfWinner(int row, int col)
        {
            return CheckDirection(row, col, 1, 0) ||
                    CheckDirection(row, col, 0, 1) ||
                    CheckDirection(row, col, 1, 1) ||
                    CheckDirection(row, col, 1, -1);
        }

        // Checks forward and backward of the piece in a specified direction for four in a row
        public bool CheckDirection(int row, int col, int rowDelta, int colDelta)
        {
            bool contForward = true, contBackward = true;
            int total = -1, forwardRow = row, forwardCol = col, backwardRow = row, backwardCol = col;

            while (contForward || contBackward)
            {
                // Check if we hit side of board 
                if (forwardRow >= height || forwardRow < 0 || forwardCol >= width || forwardCol < 0)
                    contForward = false;

                if (backwardRow >= height || backwardRow < 0 || backwardCol >= width || backwardCol < 0)
                    contBackward = false;


                // Check if the piece if the same color
                if (contForward && board[forwardRow, forwardCol].Color == currentColor)
                    total++;
                else
                    contForward = false;

                if (contBackward && board[backwardRow, backwardCol].Color == currentColor)
                    total++;
                else
                    contBackward = false;


                forwardRow += rowDelta;
                forwardCol += colDelta;

                backwardRow -= rowDelta;
                backwardCol -= colDelta;
            }

            return total >= 4;
        }

        public bool CheckIfBoardIsFull()
        {
            for (int i = 0; i < columns.Length; i++)
            {
                if (columns[i] != height)
                    return false;
            }
            gameInSession = false;
            return true;
        }
        public void SwapColor(ref Color color)
        {
            if (color == Color.YELLOW)
                color = Color.RED;
            else
                color = Color.YELLOW;
        }

        public bool IsGameOver()
        {
            return !gameInSession;
        }
    }
}
