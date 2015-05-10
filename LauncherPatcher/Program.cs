using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherPatcher
{
    class Program
    {
        static int findList(List<byte> sub, List<byte> ob)
        {
            for (var i = 0; i < ob.Count() - sub.Count(); i++)
            {
                //Console.Out.WriteLine("Searching " + i.ToString("X4"));
                var found = true;
                for (var j = 0; j < sub.Count(); j++)
                {
                    if (ob[j + i] != sub[j])
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                {
                    return i;
                }
            }
            return -1;
        }
        static List<byte> wideBytesFromString(String str){
            var ret = new List<byte>(Encoding.ASCII.GetBytes(str));
            for (var i = ret.Count(); i > 0; i-- )
            {
                ret.Insert(i, 0x00);
            }
            return ret;
        }
        static void Main(string[] args)
        {
            var origBytes = File.ReadAllBytes("DoritoPatcherWPF.exe");
            var origList = new List<byte>(origBytes);
            var origString = wideBytesFromString("https://stats.halo.click/servers");
            Console.Out.WriteLine("finding");
            for (var i = 0; i < origString.Count(); i++)
            {
                Console.Out.Write((char)origString[i]);
            }
            var index = findList(origString, origList);
            if (index > 0)
            {
                Console.Out.WriteLine(index.ToString("X4"));
                //var index = 0x2A61916;
                var newBytes = wideBytesFromString("http://browser2.crdnl.me/wp.html");
                origList.RemoveRange(index, origString.Count());
                //var newBytes = new List<byte> { 0x68, 0x00, 0x74, 0x00, 0x74, 0x00, 0x70, 0x00, 0x3A, 0x00, 0x2F, 0x00, 0x2F, 0x00, 0x62, 0x00, 0x72, 0x00, 0x6F, 0x00, 0x77, 0x00, 0x73, 0x00, 0x65, 0x00, 0x72, 0x00, 0x32, 0x00, 0x2E, 0x00, 0x63, 0x00, 0x72, 0x00, 0x64, 0x00, 0x6E, 0x00, 0x6C, 0x00, 0x2E, 0x00, 0x6D, 0x00, 0x65, 0x00, 0x2F, 0x00, 0x77, 0x00, 0x70, 0x00, 0x2e, 0x00, 0x68, 0x00, 0x74, 0x00, 0x6D, 0x00, 0x6C, 0x00};
                origList.InsertRange(index, newBytes);
                File.WriteAllBytes("DoritoPatcherWPFPatched.exe", origList.ToArray());
            }
            else
            {
                Console.Out.Write("Not found.");
            }
            
        }
    }
}
