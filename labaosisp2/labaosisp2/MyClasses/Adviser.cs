using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClasses
{
    [Serializable]
    public class Adviser : Man
    {
        private static int pQuality_Of_Advise;
        public int Quality_Of_Advise
        {
            get { return pQuality_Of_Advise; }
            set { pQuality_Of_Advise = value; }
        }

        private string pType_Of_Adviser;
        public string Type_Of_Adviser
        {
            get { return pType_Of_Adviser; }
            set { pType_Of_Adviser = value; }
        }
        static bool Advise()
        {
            if (pQuality_Of_Advise > 50)
                return true;
            else return false;
        }
        public Adviser(int qualityadvise,string typeadviser,Man man)
        {
            Quality_Of_Advise = qualityadvise;
            Type_Of_Adviser = typeadviser; First_Name = man.First_Name;
            First_Name = man.First_Name;
            Second_Name = man.Second_Name;
            Salary = man.Salary;
            Number = man.Number;
            Character = man.Character;
            Work_Schedule = man.Work_Schedule;
        }

        public Adviser(string name):base() {
            First_Name = name;
            Type_Of_Adviser = "";
            Quality_Of_Advise = 0;
        }
        public Adviser() { }
        public override string ToString() { return First_Name; }
    }

}
