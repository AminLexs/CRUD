using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClasses
{
    [Serializable]
    public class Man
    {
        private string pFirst_Name;
        public string First_Name
        {
            get { return pFirst_Name; }
            set { pFirst_Name = value; }
        }

        private string pSecond_Name;
        public string Second_Name
        {
            get { return pSecond_Name; }
            set { pSecond_Name = value; }
        }

        private int pSalary;
        public int Salary
        {
            get { return pSalary; }
            set { pSalary = value; }
        }

        private string pNumber;
        public string Number
        {
            get { return pNumber; }
            set { pNumber = value; }
        }

        private string pCharacter;
        public string Character
        {
            get { return pCharacter; }
            set { pCharacter = value; }
        }

        private string pWork_Schedule;
        public string Work_Schedule
        {
            get { return pWork_Schedule; }
            set { pWork_Schedule = value; }
        }
        
        public Man(string firstname,string secondname,int salary, string number,string character,string workschedule)
        {
            pFirst_Name = firstname;
            pSecond_Name = secondname;
            pSalary = salary;
            pNumber = number;
            pCharacter = character;
            pWork_Schedule = workschedule;
        }
        public Man(string name)
        {
            pFirst_Name = name;
        }
        public Man(){
            pFirst_Name = "Новый человек";
            pSecond_Name = "";
            pSalary = 0;
            pNumber = "+";
            pCharacter = "";
            pWork_Schedule = "";
        }
        public override string ToString() { return pFirst_Name; }

    }
}
