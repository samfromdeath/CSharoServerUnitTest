using cSharpServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSharpServerUnitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //RegisterUser("Samuel", "Test");
            var server = (Database.CurrentServer = new Server());

            server.Start();

            server.HandleCommands();
        }

        public static void RegisterUser(string username, string password)
        {
            BinaryBuffer buff = new BinaryBuffer();

            buff.BeginWrite();

            buff.Write(1);
            buff.WriteField("Password");
            buff.Write((byte)ClientArgument.ClientArgumentTypes._String);
            buff.WriteField(password);

            buff.EndWrite();

            File.WriteAllBytes(string.Format(@"data/users/{0}.svpref", username), buff.ByteBuffer);            
        }
    }
}
