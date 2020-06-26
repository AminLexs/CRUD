using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
namespace labaosisp2.Serializers
{
    class MySeril : ISerializer
    {
        private string pName = "Текстовый тип";
        public string getName()
        {
            return pName;
        }
        public string getExt()
        {
            return ".txt";
        }
        private string recurslisttotext(string result, object obj, int tire)
        {
            string str = result;
            Type type = obj.GetType();
            for (int i = 0; i < tire; i++) str += "—";
            if (str.Length != 0)
            {
                if (str[str.Length - 1] != '{')
                    str += type.ToString() + Environment.NewLine;
            }
            else str += type.ToString() + Environment.NewLine;
            List<PropertyInfo> props = new List<PropertyInfo>(type.GetProperties());

            foreach (PropertyInfo prop in props)
            {
                str += "   " + prop.Name + "=";
                if (prop.PropertyType.Name.ToString() != "String" && prop.PropertyType.Name.ToString() != "Int32")
                {
                    str += tire + "{" + Environment.NewLine;
                    str = recurslisttotext(str, prop.GetValue(obj), tire + 1);
                    str += "}" + tire + Environment.NewLine;
                }
                else
                {
                    str += prop.GetValue(obj) + Environment.NewLine;

                }
            }
            return str;
        }
        public bool Serialize(List<object> listobj, string pathname, Form1 form)
        {
            try
            {
                using (FileStream fs = new FileStream(pathname + ".txt", FileMode.OpenOrCreate))
                {
                    string str = "";
                    for (int i = 0; i < form.listBox1.Items.Count; i++)
                    {
                        str += "=====" + Environment.NewLine;
                        str = recurslisttotext(str, form.listBox1.Items[i], 0);

                    }
                    byte[] array = System.Text.Encoding.Default.GetBytes(str);
                    fs.Write(array, 0, array.Length);
                    MessageBox.Show("Объекты сериализованы", "Cериализация", MessageBoxButtons.OK);
                    // Console.WriteLine("Объекты сериализованы");
                }
                return true;
            }
            catch { return false; }
        }
        private void foragregation(object obj, StreamReader fsr)
        {

            string line = fsr.ReadLine();
            line = fsr.ReadLine();
            Type newtype = obj.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(newtype.GetProperties());
            foreach (PropertyInfo prop in props)
            {
                string[] linesfromline = line.Split(new char[] { '=' });
                string correctline = "";
                correctline = linesfromline[1];
                if (prop.PropertyType.Name.ToString() == "String")
                    prop.SetValue(obj, correctline);
                else if (prop.PropertyType.Name.ToString() == "Int32")
                    prop.SetValue(obj, Convert.ToInt32(correctline));
                else
                {
                    object agregobj = Activator.CreateInstance(prop.PropertyType, "Новый объект");
                    foragregation(agregobj, fsr);
                    prop.SetValue(obj, agregobj);
                }
                line = fsr.ReadLine();
            }

        }
        public bool Deserialize(string pathname,  Form1 form)
        {
            try
            {
                using (StreamReader fsr = new StreamReader(pathname, System.Text.Encoding.Default))
                {
                    string line;
                    Type newtype = null;
                    object newobj = null;
                    int k = 0;
                    while ((line = fsr.ReadLine()) != null)
                    {
                        if (line == "=====")
                        {
                            if ((line = fsr.ReadLine()) != null)
                            {
                                newtype = Type.GetType(line);
                                newobj = Activator.CreateInstance(newtype, "Новый объект");
                                k = 0;
                                //Console.WriteLine(line);
                                form.listBox1.Items.Add(newobj);
                            }
                        }
                        else
                        {
                            try
                            {
                                string[] linesfromline = line.Split(new char[] { '=' });
                                string correctline = linesfromline[1];

                                IList<PropertyInfo> props = new List<PropertyInfo>(newtype.GetProperties());

                                if (props[k].PropertyType.Name.ToString() == "String")
                                    props[k].SetValue(newobj, correctline);
                                else if (props[k].PropertyType.Name.ToString() == "Int32")
                                    props[k].SetValue(newobj, Convert.ToInt32(correctline));
                                else
                                {
                                    object agregobj = Activator.CreateInstance(props[k].PropertyType, "Новый объект");
                                    foragregation(agregobj, fsr);
                                    props[k].SetValue(newobj, agregobj);
                                }
                                k++;

                            }
                            catch
                            {
                                MessageBox.Show("Не получилось разбить строку или присвоить значение объекту", "Ошибка десериализации", MessageBoxButtons.OK);
                            }
                        }
                      //  Console.WriteLine(line);
                    }
                   form.UpdateListBox(form.listBox1);
                    MessageBox.Show("Объекты десериализованы", "Десериализация", MessageBoxButtons.OK);
                }
                return true;
            }
            catch { return false; }
        }
        public override string ToString() { return pName; }
    }
}
