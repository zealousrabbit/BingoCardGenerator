using System.Collections.Generic;

namespace BingoCardGenerator.Models
{
    public class BingoRequest
    {
        public int Dimension { get; set; }
        public List<string> Phrases { get; set; }
        public bool IncludeFreeSpace { get; set; }
        public int NumberOfCards { get; set; }
    }
}