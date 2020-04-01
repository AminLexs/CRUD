using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using MyClasses;
namespace labaosisp2
{
    public partial class Form2 : Form
    {
        public Form2(System.Reflection.ConstructorInfo[] constructors)
        {
            InitializeComponent();
            comboBox1.Items.AddRange(constructors);
            int i = 0;
            while(i < constructors.Length)
            {
                if (constructors[i].ToString() == "Void .ctor(System.String)")
                {
                    comboBox1.SelectedIndex = i;
                    i = constructors.Length;
                }
                i++;
            }
            constrs = constructors;

        }
        public System.Reflection.ConstructorInfo[] constrs;

        private void Button1_Click(object sender, EventArgs e)
        {
            
            Type MyType = Type.GetType("MyClasses." + ((Form1)this.Tag).comboBox2.Text);
            object myobj;
            string str = comboBox1.Items[comboBox1.SelectedIndex].ToString();
            if (str.Equals("Void .ctor()")) {
                myobj = Activator.CreateInstance(MyType);
            }
            else myobj = Activator.CreateInstance(MyType, textBox1.Text);

            ((Form1)this.Tag).listBox1.Items.Add(myobj);
            

            Close();
        }
    }
}
