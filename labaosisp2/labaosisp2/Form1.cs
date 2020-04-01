using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using MyClasses;
using labaosisp2.Serializers;
using PluginInterface;
using System.Security.Cryptography;
namespace labaosisp2
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            RefreshPlugins();
            comboBox3.Items.AddRange(plugins.ToArray());
            comboBox3.SelectedIndex = 0;
        }
        private object buffobject;
        private readonly string pluginPath = System.IO.Path.Combine(
                                                Directory.GetCurrentDirectory(),
                                                "Plugins");
        private List<IPlugin> plugins = new List<IPlugin>();
        private void RefreshPlugins()
        {
            plugins.Clear();
            
            DirectoryInfo pluginDirectory = new DirectoryInfo(pluginPath);
            if (!pluginDirectory.Exists)
                pluginDirectory.Create();   
            var pluginFiles = Directory.GetFiles(pluginPath, "*.dll");
            foreach (var file in pluginFiles)
            {
                try
                {
                    Assembly asm = Assembly.LoadFrom(file);
                    var types = asm.GetTypes().
                                    Where(t => t.GetInterfaces().
                                    Where(i => i.FullName == typeof(IPlugin).FullName).Any());

                    foreach (var type in types)
                    {
                        var plugin = asm.CreateInstance(type.FullName) as IPlugin;
                        plugins.Add(plugin);
                    }
                }
                catch { }
            }
        }
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type myType = typeof(Man);
            System.Reflection.PropertyInfo[] myPropertyInfo;
            myPropertyInfo = myType.GetProperties();
            dataGridView1.Columns.Clear();
            for (int i = 0; i < myPropertyInfo.Length; i++)
                dataGridView1.Columns.Add(myPropertyInfo[i].Name, myPropertyInfo[i].Name);
        }
        static string GetNameSeril(ISerializer seril)
        { 
            return seril.getName();
        }
        static string getSerilExtension(ISerializer seril)
        {
            return seril.getExt();
        }
        static string getPlugExtension(IPlugin plug)
        {
            return plug.getExt();
        }
        static bool StartSeril(ISerializer seril,List<object> listobj,string path,Form1 form)
        {
            
            return seril.Serialize(listobj, path,form);
        }
        static bool StartDeseril(ISerializer seril, string path,  Form1 form)
        {
            return seril.Deserialize(path,form);
        }
        private void Create_Click(object sender, EventArgs e)
        {
            string ClassName;
            ClassName = comboBox2.Text;
            try
            {
                Type MyType = Type.GetType("MyClasses." + ClassName);
                Activator.CreateInstance(MyType);
                System.Reflection.ConstructorInfo[] constructors = MyType.GetConstructors();
                Form2 constrchoice = new Form2(constructors);
                constrchoice.Show();
                constrchoice.Tag = this;

            }
            catch (Exception)
            {
                MessageBox.Show("Нет данного класса", "Ошибка", MessageBoxButtons.OK);

            }
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listBox1.SelectedIndex != -1)
            {
                buffobject = listBox1.SelectedItem;
                back.Hide();
                
                listBox2.Items.Clear();
                object myobj = listBox1.Items[listBox1.SelectedIndex];
                Type myType = myobj.GetType();
                dataGridView1.Columns.Clear();
                PropertyInfo[] myPropertyInfo;
                myPropertyInfo = myType.GetProperties();

                for (int i = 0; i < myPropertyInfo.Length; i++)
                {
                    dataGridView1.Columns.Add(myPropertyInfo[i].Name, myPropertyInfo[i].Name);

                }
                IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
                int k = 0;
                dataGridView1.Rows.Add();
                foreach (PropertyInfo prop in props)
                {
                    object propValue = prop.GetValue(myobj, null);
                    dataGridView1[k, 0].Value = propValue;
                    k++;
                }
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if ((listBox1.SelectedIndex != -1))
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                dataGridView1.Columns.Clear();
            }
        }

        public void UpdateListBox(ListBox listb)
        {
            object[] listitems=new object[listBox1.Items.Count];
            
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                listitems[i] = listBox1.Items[i];
            }
            
            listb.Items.Clear();
            listb.Items.AddRange(listitems);
        }
        private void Update_Click(object sender, EventArgs e)
        {
            if (buffobject != null)
            {
                object myobj = buffobject;
                Type myType = myobj.GetType();
                IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
                int k = 0;
                foreach (PropertyInfo prop in props)
                {
                    if (prop.PropertyType.Name.ToString() == "String")
                        try
                        {
                            prop.SetValue(myobj, dataGridView1[k, 0].Value);
                        }
                        catch
                        {
                            MessageBox.Show("Неверный формат", "Ошибка", MessageBoxButtons.OK);
                        }
                    else if (prop.PropertyType.Name.ToString() == "Int32")
                        try
                        {
                            prop.SetValue(myobj, Convert.ToInt32(dataGridView1[k, 0].Value.ToString()));
                        }
                        catch
                        {
                            MessageBox.Show("Неверный формат", "Ошибка", MessageBoxButtons.OK);
                        }
                    else
                    {
                        if (listBox2.SelectedIndex != -1)
                            try
                            {
                                prop.SetValue(myobj, dataGridView1[k, 0].Value);
                            }
                            catch
                            {
                                MessageBox.Show("Неверный формат", "Ошибка", MessageBoxButtons.OK);
                            }
                    }
                    k++;
                }
                UpdateListBox(listBox1);

            }
            
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            object myobj = dataGridView1.CurrentCell.Value;
            
            if (myobj != null)
            {

                Type myType = myobj.GetType();
                if (myType.Name != "String" && myType.Name != "Int32" && myType.Name != "Char")
                {
                    buffobject = dataGridView1.CurrentCell.Value;
                    PropertyInfo[] myPropertyInfo = myType.GetProperties();
                    back.Show();
                    dataGridView1.Columns.Clear();
                    for (int i = 0; i < myPropertyInfo.Length; i++)
                    {
                        dataGridView1.Columns.Add(myPropertyInfo[i].Name, myPropertyInfo[i].Name);
                    }
                    dataGridView1.Rows.Add();
                    if (myobj != null)
                    {
                        int k = 0;
                        foreach (PropertyInfo prop in myPropertyInfo)
                        {
                            object propValue = prop.GetValue(myobj, null);
                            dataGridView1[k, 0].Value = propValue;
                            k++;
                        }
                    }
                    listBox2.Items.Clear();
                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        Type listitemtype = listBox1.Items[i].GetType();
                        if (listitemtype == myobj.GetType())
                        {
                            listBox2.Items.Add(listBox1.Items[i]);
                        }

                    }
                }

            }
         
        }

        private void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                object myobj = listBox2.Items[listBox2.SelectedIndex];
                Type myType = myobj.GetType();
                List<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
                int k = 0;
                foreach (PropertyInfo prop in props)
                {
                    object propValue = prop.GetValue(myobj, null);
                    dataGridView1[k, 0].Value = propValue;
                    k++;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            Type[] alltypes = Assembly.GetExecutingAssembly().GetTypes();
            foreach (Type currtype in alltypes)
            {
                if (currtype.FullName.Contains("MyClasses"))
                    comboBox2.Items.Add(currtype.Name);
                if (currtype.FullName.Contains("Serializers") && !currtype.FullName.Contains("ISerializer"))
                {
                    Binary b = new Binary();
                    object someseril= Activator.CreateInstance(currtype);
                    //GetNameSeril(b);
                    comboBox1.Items.Add(someseril);
                }
            }
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            back.Hide();
            Man man=new Man("Василий","Кошкин",1000,"+37533904804","Решительный","Пн-Пт 9:00-17:00");
            listBox1.Items.Add(man);
            Director director=new Director(null,40,30,null,man);
            listBox1.Items.Add(director);
            man =new Man("Пётр", "Плюшкин", 500, "+37533934534", "Добрый", "Пн-Пт 8:00-17:00");
            Adviser adv = new Adviser(70,"По поеданиям пирожков",man);
            listBox1.Items.Add(adv);
            director.DirAdviser = adv;
            Company comp = new Company("Мир сладостей", null, 10000000, director);
            director.Ownership = comp;
            listBox1.Items.Add(comp);
            Product prod = new Product("Пирожки", 100, 5, 30);
            listBox1.Items.Add(prod);
            comp.Production = prod;
            man = new Man("Александр", "Котелков", 200, "+37529544529", "Опытный", "Пн-Пт 7:00-16:00");
            Worker worker = new Worker("Повар", 90, man);
            listBox1.Items.Add(worker);
            Engineer engineer = new Engineer("Шеф-повар", comp, prod, worker);
            listBox1.Items.Add(engineer);
            man=new Man("Виктория", "Волочкова", 300, "+37533553929", "Ленивая", "Вт-Сб 10:00-18:00");
            worker = new Worker("Бухгалтер", 40, man);
            Office_Worker offworker = new Office_Worker("Бухгалтерия", comp, worker);
            listBox1.Items.Add(offworker);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            
            back.Hide();
            Type myType = buffobject.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            int k = 0;
            foreach (PropertyInfo prop in props)
            {
                if (prop.PropertyType.Name.ToString() == "String")
                    try
                    {
                        prop.SetValue(buffobject, dataGridView1[k, 0].Value);
                    }
                    catch
                    {
                        MessageBox.Show("Неверный формат", "Ошибка", MessageBoxButtons.OK);
                    }
                else if (prop.PropertyType.Name.ToString() == "Int32")
                    try
                    {
                        prop.SetValue(buffobject, Convert.ToInt32(dataGridView1[k, 0].Value.ToString()));
                    }
                    catch
                    {
                        MessageBox.Show("Неверный формат", "Ошибка", MessageBoxButtons.OK);
                    }
                else try
                    {
                        prop.SetValue(buffobject, dataGridView1[k, 0].Value);
                    }
                    catch
                    {
                        MessageBox.Show("Неверный формат", "Ошибка", MessageBoxButtons.OK);
                    }
                k++;
            }
            object selectedobject= listBox1.SelectedItem;
            myType = selectedobject.GetType();
            props = new List<PropertyInfo>(myType.GetProperties());
            listBox2.Items.Clear();
            dataGridView1.Columns.Clear();
            PropertyInfo[] myPropertyInfo = myType.GetProperties();
            

            for (int i = 0; i < myPropertyInfo.Length; i++)
            {
                dataGridView1.Columns.Add(myPropertyInfo[i].Name, myPropertyInfo[i].Name);

            }
            props = new List<PropertyInfo>(myType.GetProperties());
            k = 0;
            dataGridView1.Rows.Add();
            foreach (PropertyInfo prop in props)
            {
                object propValue = prop.GetValue(selectedobject, null);
                dataGridView1[k, 0].Value = propValue;
                k++;
            }
            buffobject = listBox1.SelectedItem;
        }


        private void Seril_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

              //  IPlugin plug= comboBox3.Items[1] as IPlugin;
           
          //      string sssss = plug.Encrypt("ОВОЩЬ SOSI");
             //   string rrrrrr= plug.Decrypt(sssss);
                
                List<object> list = new List<object>();
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    list.Add(listBox1.Items[i]);
                }
                StartSeril((ISerializer)comboBox1.SelectedItem, list, saveFileDialog1.FileName, (Form1)FindForm());
                if (comboBox3.SelectedIndex != 0)
                {
                    object crypto = comboBox3.SelectedItem;
                    //string str;
                    byte[] encrByteFile;
                    using (StreamReader fsr = new StreamReader(saveFileDialog1.FileName + getSerilExtension((ISerializer)comboBox1.SelectedItem), System.Text.Encoding.Default))
                    {
                        string buf = fsr.ReadToEnd();
                        encrByteFile = (crypto as IPlugin).Encrypt(buf, saveFileDialog1.FileName);
                    }

                    // using (FileStream fsw = new FileStream(saveFileDialog1.FileName+getPlugExtension((IPlugin)crypto), FileMode.Create))
                    //     {
                    // byte[] array = System.Text.Encoding.UTF8.GetBytes(str);
                    File.WriteAllBytes(saveFileDialog1.FileName + getPlugExtension((IPlugin)crypto), encrByteFile);
                    
                  //  StreamWriter SW = new StreamWriter(new FileStream(saveFileDialog1.FileName + getPlugExtension((IPlugin)crypto), FileMode.Create, FileAccess.Write));
                    //    SW.Write(str);
                      //  SW.Close();
                       // fsw.Write(array, 0, array.Length);
                 //   }
                }
            }
        }

        
        private void Deseril_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {               
                listBox1.Items.Clear();
                if (!checkBox1.Checked)
                {                   
                    StartDeseril((ISerializer)comboBox1.SelectedItem, openFileDialog1.FileName, (Form1)FindForm());
                }
                else
                {
                    object crypto=null;              
                    int i = 1;
                        while (i < comboBox3.Items.Count)
                        {
                        if ((comboBox3.Items[i] as IPlugin).getExt() == Path.GetExtension(openFileDialog1.FileName))
                        {
                            crypto = comboBox3.Items[i];
                            i = comboBox3.Items.Count;
                        }
                      
                        i++;
                        }
                    if (crypto != null)
                    {
                        byte[] bytestodecrypt = File.ReadAllBytes(openFileDialog1.FileName);
                        string str = (crypto as IPlugin).Decrypt(bytestodecrypt, openFileDialog1.FileName);

                        using (FileStream fsw = new FileStream("decryptbuffile.decr", FileMode.OpenOrCreate))
                        {

                            byte[] array = System.Text.Encoding.Default.GetBytes(str);
                            fsw.Write(array, 0, array.Length);
                        }
                        StartDeseril((ISerializer)comboBox1.SelectedItem, "decryptbuffile.decr", (Form1)FindForm());

                        System.IO.File.Delete("decryptbuffile.decr");
                    }
                    else MessageBox.Show("Плагин не найден!", "Ошибка плагинов", MessageBoxButtons.OK);

                }
            }
        }
    }
}
