using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uninst
{
    class Program
    {
        static void Main(string[] args)
        {
            string sysroot = System.Environment.SystemDirectory; System.Diagnostics.Process.Start(sysroot + "\\msiexec.exe", "/x {9992CB7D-5C92-4848-84EA-49CA97A2071D} /qr"); 
        }
    }
}
