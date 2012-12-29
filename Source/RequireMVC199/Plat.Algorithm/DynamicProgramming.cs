using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plat.Algorithm
{
    public class DynamicProgramming
    {
        private int[,] node = new int[2,6];
        private int[,] trans = new int[2,5];
        private int e1, e2, x1, x2;

        public DynamicProgramming()
        {
            Initialize();
        }

        private void Initialize()
        {
            e1 = 2;
            e2 = 4;
            x1 = 3;
            x2 = 2;

            trans[0, 0] = 2;
            trans[0, 1] = 3;
            trans[0, 2] = 1;
            trans[0, 3] = 3;
            trans[0, 4] = 4;

            trans[1, 0] = 2;
            trans[1, 1] = 1;
            trans[1, 2] = 2;
            trans[1, 3] = 2;
            trans[1, 4] = 1;

            node[0, 0] = 7;
            node[0, 1] = 9;
            node[0, 2] = 3;
            node[0, 3] = 4;
            node[0, 4] = 8;
            node[0, 5] = 4;

            node[1, 0] = 8;
            node[1, 1] = 5;
            node[1, 2] = 6;
            node[1, 3] = 4;
            node[1, 4] = 5;
            node[1, 5] = 7;

        }

        public int FindShortestPath()
        {
            int line = -1;
            var result = GetMin(GetNodeValue(0, 5) + node[0, 5] + x1, GetNodeValue(1, 5) + node[1, 5] + x2, out line);
            Console.WriteLine(string.Format("result:{0}, from line:{1}", result, line));
            return result;
        }

        private int[,] shortestValue = new int[2, 6];
        private int GetNodeValue(int row, int column)
        {
            if (shortestValue[row, column] != 0)
                return shortestValue[row, column];

            var result = 0;
            if (row == 0 && column == 0)
            {
                result = e1;
                shortestValue[row, column] = result;
                return result;
            }

            if (row == 1 && column == 0)
            {
                result = e2;
                shortestValue[row, column] = result;
                return result;
            }

            if (row == 0 && column > 0)
            {
                int line = -1;
                int x = GetNodeValue(0, column - 1) + node[0, column-1];
                int y = GetNodeValue(1, column - 1) + trans[1, column - 1] + node[1, column-1];
                result = GetMin(x, y, out line);
                shortestValue[row, column] = result;
                Console.WriteLine(string.Format("rows:{0}, columns:{1}, result:{2}, from line:{3}", row, column, result, line));
                return result;
            }

            if (row == 1 && column > 0)
            {
                int line = -1;
                int m = GetNodeValue(0, column - 1) + trans[0, column - 1] + node[0, column - 1];
                int n = GetNodeValue(1, column - 1) + node[1, column - 1];
                result = GetMin(m, n, out line);
                shortestValue[row, column] = result;
                Console.WriteLine(string.Format("rows:{0}, columns:{1}, result:{2}, from line:{3}", row, column, result, line));
                return result;
            }
            return result;
        }

        private int GetMin(int x, int y, out int line)
        {
            if (x <= y)
            {
                line = 0;
                return x;
            }
            else
            {
                line = 1;
                return y;
            }
        }
    }
}
