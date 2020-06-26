using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
namespace labaosisp2.Serializers
{
    class Binary:ISerializer
    {
        private string pName="Бинарный";
     public string getName()
        {
            return pName;
        }
     public string getExt()
     {
            return ".dat";
     }
    public bool Serialize(List<object> listobj, string pathname, Form1 form)
        {
            try
            {
                MyClassCollection listclasses = new MyClassCollection();
                listclasses.Collection = listobj;
                BinaryFormatter binaryF = new BinaryFormatter();
                FileStream fs = new FileStream(pathname + ".dat", FileMode.OpenOrCreate);
                using (fs)
                {
                    binaryF.Serialize(fs, listclasses);
                    MessageBox.Show("Объекты сериализованы", "Cериализация", MessageBoxButtons.OK);
                    // Console.WriteLine("Объекты сериализованы");
                }
                return true;
            }
            catch { return false; }
        }
     public bool Deserialize(string pathname, Form1 form)
        {
            try
            {
                BinaryFormatter binaryF = new BinaryFormatter();
                FileStream fs = new FileStream(pathname, FileMode.OpenOrCreate);
                using (fs)
                {
                    MyClassCollection obj = (MyClassCollection)binaryF.Deserialize(fs);
                    object[] objs = obj.Collection.ToArray();
                    form.listBox1.Items.AddRange(objs);
                    MessageBox.Show("Объекты десериализованы", "Десериализация", MessageBoxButtons.OK);
                    
                }
                return true;
            }
            catch { return false; }
        }
        public override string ToString() { return pName; }

    }
}
