using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plat.Algorithm
{
    public class Program
    {
        public static void Main()
        {
            DynamicProgramming p = new DynamicProgramming();
            var result = p.FindShortestPath();
            Console.WriteLine(result);
            Console.ReadLine();
            
        }
    }
}
