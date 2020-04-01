using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginInterface
{
    public interface IPlugin
    {
        string getExt();
        byte[] Encrypt(string input,string pathkey);
        string Decrypt(byte[] output,string pathkey);
    }
}
