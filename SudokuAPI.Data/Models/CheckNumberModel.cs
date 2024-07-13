using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuAPI.Data.Models
{
    public class CheckNumberModel
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public int Number { get; set; }
    }
}
