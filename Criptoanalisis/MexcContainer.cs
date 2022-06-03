using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApiTests
{
    internal class MexcContainer
    {
        public int Code { get; set; }
        public List<Symbols> Data { get; set; }
    }
}
