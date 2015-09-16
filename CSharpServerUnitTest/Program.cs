using cSharpServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpServerUnitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = (Database.CurrentServer = new Server());

            server.Start();

            server.HandleCommands();
        }
    }
}
