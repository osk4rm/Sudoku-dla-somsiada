using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuAPI.Data.Observers
{
    public interface IGameObserver
    {
        void Notify(bool isGameFinished);
    }

    public class GameObserver : IGameObserver
    {
        public bool IsGameFinished { get; private set; }

        public void Notify(bool isGameFinished)
        {
            IsGameFinished = isGameFinished;
            Console.WriteLine(isGameFinished ? "Koniec gry" : "Gramy dalej");
        }
    }
}
