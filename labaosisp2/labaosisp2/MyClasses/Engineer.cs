using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClasses
{
    [Serializable]
    public class Engineer:Worker
    {
        private string pQualification;
        public string Qualification
        {
            get { return pQualification; }
            set { pQualification = value; }
        }

        private Company pFactory;
        public Company Factory
        {
            get { return pFactory; }
            set { pFactory = value; }
        }

        private Product pProduct;
        public Product Product
        {
            get { return pProduct; }
            set { pProduct = value; }
        }

        static bool Produce()
        {
            return true;
        }

        public Engineer(string name) : base()
        {
            First_Name = name;
            pFactory = new Company("");
            pProduct = new Product("");
        }
        public Engineer(string qual,Company comp,Product prod,Worker worker) : base()
        {
            Qualification = qual;
            Factory = comp;
            Product = prod;
            First_Name = worker.First_Name;
            Second_Name = worker.Second_Name;
            Salary = worker.Salary;
            Number = worker.Number;
            Character = worker.Character;
            Work_Schedule = worker.Work_Schedule;
            Rating = worker.Rating;
            Post = worker.Post;

        }
        public Engineer() { }
    }
}
