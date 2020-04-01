using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClasses
{
    [Serializable]
    public class Company
    {
        private string pName;
        public string Name
        {
            get { return pName; }
            set { pName = value; }
        }

        private Product pProduction;
        public Product Production
        {
            get { return pProduction; }
            set { pProduction = value; }
        }

        private int pBudjet;
        public int Budjet
        {
            get { return pBudjet; }
            set { pBudjet = value; } 
        }

        public Company(string name,Product prod,int budj,Director dir)
        {
            Name = name;
            pProduction = prod;
            Budjet = budj;
        }
        public Company(string name)
        {
            pName = name;
            pProduction = new Product("");
        }
        public Company() { }
        public override string ToString() { return Name; }
    }
}
