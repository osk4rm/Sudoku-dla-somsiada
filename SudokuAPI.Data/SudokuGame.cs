using SudokuAPI.Data.Observers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuAPI.Data;

public class SudokuGame
{
    private static readonly Random Random = new Random();
    public int[,] Board { get; private set; }
    public int[,] Solution { get; private set; }
    private readonly List<IGameObserver> observers = new List<IGameObserver>();

    public SudokuGame()
    {
        Board = new int[9, 9];
        Solution = new int[9, 9];
        GenerateSudoku();
    }

    private void GenerateSudoku()
    {
        Solution = new int[9, 9]
        {
            {5,3,4,6,7,8,9,1,2},
            {6,7,2,1,9,5,3,4,8},
            {1,9,8,3,4,2,5,6,7},
            {8,5,9,7,6,1,4,2,3},
            {4,2,6,8,5,3,7,9,1},
            {7,1,3,9,2,4,8,5,6},
            {9,6,1,5,3,7,2,8,4},
            {2,8,7,4,1,9,6,3,5},
            {3,4,5,2,8,6,1,7,9}
        };

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (Random.Next(0, 2) == 1)
                {
                    Board[i, j] = Solution[i, j];
                }
            }
        }
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
