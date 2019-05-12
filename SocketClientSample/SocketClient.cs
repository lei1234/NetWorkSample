using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketClientSample
{
    public class SocketClient
    {
        private Socket _clientSocket;
        private string _ip;
        private int _port;

        public SocketClient(int port)
        {
            _ip = "127.0.0.1";
            _port = port;
        }

        public void StartClient()
        {
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress address = IPAddress.Parse(_ip);

            IPEndPoint endPoint = new IPEndPoint(address, _port);

            _clientSocket.Connect(endPoint);

            byte[] buffer = new byte[1024];

            Task.Factory.StartNew(() =>
            {
                try
                {
                    while (true)
                    {
                        var length = _clientSocket.Receive(buffer);
                        Console.WriteLine($"服务端发送的消息:{Encoding.UTF8.GetString(buffer)}");
                    }
                }
                catch (Exception ex)
                {

                }


            });

            while (true)
            {
                var input = Console.ReadLine();
                _clientSocket.Send(Encoding.UTF8.GetBytes(input));
                Console.WriteLine($"向服务器发送消息:{input}");
            }

        }
    }
}
