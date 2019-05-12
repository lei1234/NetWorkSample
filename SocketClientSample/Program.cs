using System;
using System.Threading;

namespace SocketClientSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(2000);
            SocketClient client = new SocketClient(8888);
            client.StartClient();

            Console.Read();
        }
    }
}
