using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;

namespace labaosisp2.Serializers
{
    class XML : ISerializer
    {
        private string pName = "XML";
        public string getName()
        {
            return pName;
        }
        public string getExt()
        {
            return ".xml";
        }
        public bool Serialize(List<object> listobj, string pathname, Form1 form)
        {
            try
            {
                MyClassCollection listclasses = new MyClassCollection();
                listclasses.Collection = listobj;
                using (FileStream fs = new FileStream(pathname + ".xml", FileMode.OpenOrCreate))
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(MyClassCollection));

                    formatter.Serialize(fs, listclasses);
                    MessageBox.Show("Объекты сериализованы", "Cериализация", MessageBoxButtons.OK);
                    //Console.WriteLine("Объекты сериализованы");
                }
                    return true;
            }
            catch { return false; }
        }
        public bool Deserialize(string pathname, Form1 form)
        {
            try
            {
                using (FileStream fs = new FileStream(pathname, FileMode.OpenOrCreate))
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(MyClassCollection));
                    MyClassCollection obj = (MyClassCollection)formatter.Deserialize(fs);
                    MessageBox.Show("Объекты десериализованы", "Десериализация", MessageBoxButtons.OK);
                    // Console.WriteLine("Объекты десериализованы");

                    object[] objs = obj.Collection.ToArray();
                    form.listBox1.Items.AddRange(objs);


                }
                return true;
            }
            catch { return false; }
            
            
        }
        public override string ToString() { return pName; }
    }
}
