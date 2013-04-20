using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudService.TSP
{
    public class City
    {
        private String name;
        private int x,y;

        public City(String name, int x, int y)
        {

            this.name = name;
            this.x = x;
            this.y = y;

        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }
        
        public int X
        {
            get { return x; }
            set { x = value; }
        }


        public int Y
        {
            get { return y; }
            set { y = value; }
        }
    }

}

