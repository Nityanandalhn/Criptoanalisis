using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApiTests
{
    internal class Symbols
    {
        public string? Symbol { get; set; }
        public string? State { get; set; }
        public int CountDownMark { get; set; }
        public string? TimeZone { get; set; }
        public string? FullName { get; set; }
        public int SymbolStatus { get; set; }
        public string? VcoinName { get; set; }
        public int VcoinStatus { get; set; }
        public int Price_scale { get; set; }
    }
}
