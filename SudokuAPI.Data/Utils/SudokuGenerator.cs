using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuAPI.Data.Utils
{
    public interface ISudokuGenerator
    {
        (int[,], int[,]) GenerateSudoku();
    }

    public class SudokuGenerator : ISudokuGenerator
    {
        private static readonly Random Random = new Random();

        public (int[,], int[,]) GenerateSudoku()
        {
            int[,] solution = new int[9, 9];
            int[,] board = new int[9, 9];
            FillSudoku(solution);

            Array.Copy(solution, board, solution.Length);
            RemoveNumbersFromBoard(board, 40);

            return (board, solution);
        }

        private void FillSudoku(int[,] board)
        {
            FillDiagonalBoxes(board);
            FillRemainingCells(board, 0, 3);
        }

        private void FillDiagonalBoxes(int[,] board)
        {
            for (int i = 0; i < 9; i += 3)
            {
                FillBox(board, i, i);
            }
        }

        private void FillBox(int[,] board, int rowStart, int colStart)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int num;
                    do
                    {
                        num = Random.Next(1, 10);
                    } while (!IsSafeToPlace(board, rowStart + i, colStart + j, num));

                    board[rowStart + i, colStart + j] = num;
                }
            }
        }

        private bool FillRemainingCells(int[,] board, int i, int j)
        {
            if (j >= 9 && i < 8)
            {
                i++;
                j = 0;
            }
            if (i >= 9 && j >= 9)
                return true;

            if (i < 3)
            {
                if (j < 3)
                    j = 3;
            }
            else if (i < 6)
            {
                if (j == (i / 3) * 3)
                    j = j + 3;
            }
            else
            {
                if (j == 6)
                {
                    i++;
                    j = 0;
                    if (i >= 9)
                        return true;
                }
            }

            for (int num = 1; num <= 9; num++)
            {
                if (IsSafeToPlace(board, i, j, num))
                {
                    board[i, j] = num;
                    if (FillRemainingCells(board, i, j + 1))
                        return true;

                    board[i, j] = 0;
                }
            }
            return false;
        }

        private bool IsSafeToPlace(int[,] board, int row, int col, int num)
        {
            for (int x = 0; x < 9; x++)
                if (board[row, x] == num || board[x, col] == num)
                    return false;

            int startRow = row - row % 3, startCol = col - col % 3;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (board[i + startRow, j + startCol] == num)
                        return false;

            return true;
        }

        private void RemoveNumbersFromBoard(int[,] board, int count)
        {
            for (int i = 0; i < count; i++)
            {
                int row, col;
                do
                {
                    row = Random.Next(0, 9);
                    col = Random.Next(0, 9);
                } while (board[row, col] == 0);

                board[row, col] = 0;
            }
        }
    }
}
