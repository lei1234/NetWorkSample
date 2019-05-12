using System;

namespace SocketServerSample
{
    class Program
    {
        static void Main(string[] args)
        {
            SocketServer server = new SocketServer(8888);
            server.Listen();
            Console.ReadKey();
        }
    }
}
