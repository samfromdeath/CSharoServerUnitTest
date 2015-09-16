using cSharpServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpServerUnitTest
{
    class ClientLogin : IClientLogin
    {
        /// <summary>
        /// This login uses a flat file system, data/users/{0}.user - the users name is the file.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public bool Login(byte[] data, Client client)
        {
            try
            {
                BinaryBuffer buff = new BinaryBuffer(data, true);

                client.Username = buff.ReadString(buff.ReadInt()); // Username
                client.Password = buff.ReadString(buff.ReadInt()); // Password

                buff.EndRead();

                buff = new BinaryBuffer(File.ReadAllBytes(string.Format(@"data/users/{0}.svpref", client.Username)), true);

                LoadClientArguments(ref buff, ref client);
                // does pass equal?

                string arg;
                client.GetArgument("Password", out arg);
                if (arg != client.Password)
                    return false;

                buff.EndRead();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// This function will run not matter if it errors...
        /// </summary>
        /// <param name="data"></param>
        /// <param name="client"></param>
        public void LoadClientArguments(ref BinaryBuffer data, ref Client client)
        {
            //how many arguments do we have stored?
            try
            {
                int Total = data.ReadInt();
                for (int i = 0; i < Total; i++)
                    client.Arguments.Add(new ClientArgument(data.ReadString(data.ReadInt()), data.ReadByte(), data.ReadByteArray(data.ReadInt())));
            }
            catch (Exception)
            {
                // If the file is out of order, they lose there data.
                // because if they can login then the file has been changed manually.
            }
        }
    }
}
