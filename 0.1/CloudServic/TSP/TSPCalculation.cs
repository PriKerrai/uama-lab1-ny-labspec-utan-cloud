using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudService.TSP
{
    public class TSPCalculation
    {
        public int result = 0;
        
        
        /*private City[] citiesToVisit;

        public City[] CitiesToVisit
        {
            get { return citiesToVisit; }
            set { citiesToVisit = value; }
        }*/

        public void YoloSwag()
        {
            for (int i = 0; i < 1; i++)
            {
                // simulate calculating
                System.Threading.Thread.Sleep(20000); // 20 sec
            }

            result = 1;
        }

        //public void Start(int NumberOfNodes)
        public void Start(City[] citiesToVisit)
        {
            int NumberOfNodes = citiesToVisit.Length;

            int[] x = new int[NumberOfNodes];
            int[] y = new int[NumberOfNodes];
            int[] nodes = new int[NumberOfNodes];

            Random random = new Random();
            for (int i = 0; i < NumberOfNodes; i++)
            {
                nodes[i] = i;
                x[i] = random.Next(1500);
                y[i] = random.Next(1500);

            }

            this.tsp(NumberOfNodes, x, y, nodes);

        }

        private void tsp(int NumberOfNodes, int[] x, int[] y, int[] nodes)
        {

            long tmpcost, cheapest_cost = -1;

            int[] temp = new int[NumberOfNodes];
            int[] cheapest_path = new int[NumberOfNodes];
            int i;

            do
            {
                tmpcost = 0;
                for (i = 0; i < NumberOfNodes; i++)
                {
                    temp[i] = nodes[i];
                    if (i != 0)
                    {
                        tmpcost += (long)Math.Sqrt((Math.Pow(x[(int)nodes[i]] - x[(int)nodes[i - 1]], 2)) + (Math.Pow(y[(int)nodes[i]] - y[(int)nodes[i - 1]], 2)));
                    }
                }
                if (cheapest_cost == -1 || cheapest_cost > tmpcost)
                {
                    cheapest_cost = tmpcost;
                    for (i = 0; i < NumberOfNodes; i++)
                    {
                        cheapest_path[i] = temp[i];
                    }
                }


            } while (next_permutation(nodes));

            for (i = 0; i < NumberOfNodes; i++)
            {
                Console.WriteLine(cheapest_path[i]);
            }

            result = Convert.ToInt16(cheapest_cost);
        }

        public bool next_permutation(int[] perm)
        {
            int n = perm.Length;
            int k = -1;
            for (int i = 1; i < n; i++)
                if (perm[i - 1] < perm[i])
                    k = i - 1;
            if (k == -1)
            {
                for (int i = 0; i < n; i++)
                    perm[i] = i;
                return false;
            }

            int l = k + 1;
            for (int i = l; i < n; i++)
                if (perm[k] < perm[i])
                    l = i;

            int t = perm[k];
            perm[k] = perm[l];
            perm[l] = t;

            Array.Reverse(perm, k + 1, perm.Length - (k + 1));

            return true;
        }
    }
}