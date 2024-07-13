using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuAPI.Data.Models
{
    public class CheckResultModel
    {
        public bool IsCorrect { get; set; }
        public bool IsGameFinished { get; set; }
    }
}
