using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClasses
{
    [Serializable]
    public class Worker:Man
    {
        private string pPost;
        public string Post
        {
            get { return pPost; }
            set { pPost = value; }
        }

        private int pRating;
        public int Rating
        {
            get { return pRating; }
            set { pRating = value; }
        }

        static bool Work()
        {
            return true;
        }
        public Worker(string name) : base()
        {
            pPost = "";
            pRating = 0;
            First_Name = name;
            Second_Name = "";
            Salary = 0;
            Number ="";
            Character = "";
            Work_Schedule = "";
        }
        public Worker(string post,int rating,Man man) : base()
        {
            pPost = post;
            pRating = rating;
            First_Name = man.First_Name;
            Second_Name = man.Second_Name;
            Salary = man.Salary;
            Number = man.Number;
            Character = man.Character;
            Work_Schedule = man.Work_Schedule;
        }
        public Worker() { }
    }
}
