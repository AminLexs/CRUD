using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labaosisp2.Serializers
{
    interface ISerializer
    {
        string getName();
        string getExt();
        bool Serialize(List<object> listobj, string pathname,Form1 form);
        bool Deserialize(string pathname, Form1 form);

    }
}
