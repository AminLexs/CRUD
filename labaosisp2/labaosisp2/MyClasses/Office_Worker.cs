using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClasses
{
    [Serializable]
    public class Office_Worker: Worker
    {
        private Company pOffice;
        public Company Office
        {
            get { return pOffice; }
            set { pOffice = value; }
        }

        private string pOfficeWork;
        public string OfficeWork
        {
            get { return pOfficeWork; }
            set { pOfficeWork = value; }
        }

        static bool Do_Paperwork()
        {
            return true;
        }
        public Office_Worker(string name) : base()
        {
            OfficeWork = "";
            Office = new Company("");
            First_Name =name;
            Second_Name = "";
            Salary = 0;
            Number = "";
            Character = "";
            Work_Schedule = "";
            Rating = 0;
            Post = "";
        }
        public Office_Worker(string offwork, Company comp, Worker worker) : base()
        {
            OfficeWork = offwork;
            Office = comp;
            First_Name = worker.First_Name;
            Second_Name = worker.Second_Name;
            Salary = worker.Salary;
            Number = worker.Number;
            Character = worker.Character;
            Work_Schedule = worker.Work_Schedule;
            Rating = worker.Rating;
            Post = worker.Post;

        }
        public Office_Worker() { }
    }
}
