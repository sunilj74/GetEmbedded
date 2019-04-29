using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetEmbedded
{
    class Program
    {
        static void Main(string[] args)
        {
            Utils.ExtractResources(args[1], args[0]);
        }
    }
}
