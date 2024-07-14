using SudokuAPI.Data.Observers;
using SudokuAPI.Data.Utils;
using System;
using System.Collections.Generic;

namespace SudokuAPI.Data
{
    public class SudokuGame
    {
        private readonly ISudokuGenerator _sudokuGenerator;
        public int[,] Board { get; private set; }
        public int[,] Solution { get; private set; }
        private readonly List<IGameObserver> observers = new List<IGameObserver>();

        public SudokuGame(ISudokuGenerator sudokuGenerator)
        {
            _sudokuGenerator = sudokuGenerator;
            GenerateSudoku();
        }

        private void GenerateSudoku()
        {
            (Board, Solution) = _sudokuGenerator.GenerateSudoku();
        }

        public bool CheckNumber(int row, int col, int number)
        {
            bool isCorrect = Solution[row, col] == number;

            if (isCorrect)
                Board[row, col] = number;

            NotifyObservers();

            return isCorrect;
        }

        public void AddObserver(IGameObserver observer)
        {
            observers.Add(observer);
        }

        private void NotifyObservers()
        {
            bool isFinished = IsGameFinished();

            foreach (var observer in observers)
            {
                observer.Notify(isFinished);
            }
        }

        private bool IsGameFinished()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (Board[i, j] != Solution[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public List<List<int>> GetBoardAsList()
        {
            return ConvertToList(Board);
        }

        public void ResetGame()
        {
            GenerateSudoku();
        }

        public static int[,] ConvertToArray(List<List<int>> list)
        {
            var array = new int[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    array[i, j] = list[i][j];
                }
            }
            return array;
        }

        private List<List<int>> ConvertToList(int[,] array)
        {
            var list = new List<List<int>>();
            for (int i = 0; i < 9; i++)
            {
                var row = new List<int>();
                for (int j = 0; j < 9; j++)
                {
                    row.Add(array[i, j]);
                }
                list.Add(row);
            }
            return list;
        }
    }
}
