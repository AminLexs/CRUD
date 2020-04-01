using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClasses
{
    [Serializable]
    public class Product
    {
        private string pName;
        public string Name
        {
            get { return pName; }
            set { pName = value; }
        }

        private int pPopularity;
        public int Popularity
        {
            get { return pPopularity; }
            set { pPopularity = value; }
        }

        private int pCost;
        public int Cost
        {
            get { return pCost; }
            set { pCost = value; }
        }

        private int pComplexity;
        public int Complexity
        {
            get { return pComplexity; }
            set { pComplexity = value; }
        }
        public Product(string name)
        {
            pName = name;
            pPopularity = 0;
            pCost = 0;
            pComplexity = 0;
        }
        public Product(string name,int pop,int cost,int compl)
        {
            pName = name;
            pPopularity = pop;
            pCost = cost;
            pComplexity = compl;
        }
        public Product() { }
        public override string ToString() { return Name; }
    }
}
