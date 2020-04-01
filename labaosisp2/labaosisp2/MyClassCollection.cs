using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MyClasses;
namespace labaosisp2
{
    [XmlInclude(typeof(Man)), XmlInclude(typeof(Adviser)), XmlInclude(typeof(Engineer)),
        XmlInclude(typeof(Director)), XmlInclude(typeof(Worker)), XmlInclude(typeof(Office_Worker)), XmlInclude(typeof(Product)), XmlInclude(typeof(Company))]
    
    [Serializable]
    public class MyClassCollection
    {
        [XmlArray("Collection"), XmlArrayItem("Item")]
        public List<object> Collection { get; set; }
    }
}
