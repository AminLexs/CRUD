using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClasses
{
    [Serializable]
    public class Director:Man
    {
        private Company pOwnership;
        public Company Ownership
        {
            get { return pOwnership; }
            set { pOwnership = value; }
        }

        private static int pQuality_Of_Manage;
        public int Quality_Of_Manage
        {
            get { return pQuality_Of_Manage; }
            set { pQuality_Of_Manage = value; }
        }

        private int pNumber_Of_Staff;
        public int Number_Of_Staff
        {
            get { return pNumber_Of_Staff; }
            set { pNumber_Of_Staff = value; }
        }

        private static Adviser pDirAdviser;
        public Adviser DirAdviser
        {
            get { return pDirAdviser; }
            set { pDirAdviser = value; }
        }

        static bool Manage()
        {
            if (pQuality_Of_Manage + pDirAdviser.Quality_Of_Advise > 75) return true;
            else return false;
        }
        public Director(string name) : base()
        {
            Ownership = new Company("");
            Quality_Of_Manage = 0;
            Number_Of_Staff = 0;
            DirAdviser = new Adviser("");
            First_Name = name;
            Second_Name = "";
            Salary = 0;
            Number = "";
            Character = "";
            Work_Schedule = "";
        }
        public Director(Company comp,int qmanage,int numstaff,Adviser adv,Man man):base()
        {
            Ownership = comp;
            Quality_Of_Manage = qmanage;
            Number_Of_Staff = numstaff;
            DirAdviser = adv;
            First_Name = man.First_Name;
            Second_Name = man.Second_Name;
            Salary = man.Salary;
            Number = man.Number;
            Character = man.Character;
            Work_Schedule = man.Work_Schedule;
        }
        public Director() { }
        
    }
}
