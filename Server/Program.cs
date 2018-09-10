using Server.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server {
    public class Program {

        private static ServerSocket ServerSocket = new ServerSocket();
        static void Main(string[] args) {
            ServerSocket.Bind(6556);
            ServerSocket.Listen(500);
            ServerSocket.Accept();

            while(true)
                Console.ReadLine();

        }
    }
}
