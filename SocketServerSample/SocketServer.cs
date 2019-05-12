using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketServerSample
{
    public class SocketServer
    {
        Socket _socket;

        private string ip;

        private int port;

        public SocketServer(int p)
        {
            ip = "0.0.0.0";
            port = p;
        }

        public void Listen()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress ipAddress = IPAddress.Parse(ip);

            IPEndPoint iPEndPoint = new IPEndPoint(ipAddress, port);

            _socket.Bind(iPEndPoint);

            _socket.Listen(int.MaxValue);

            Console.WriteLine($"监听{port}端口成功");

            Thread thread = new Thread(SocketConnection);

            thread.Start();
        }

        private void SocketConnection()
        {
            try
            {
                while (true)
                {
                    var clientSocket = _socket.Accept();
                    Thread thread = new Thread(ReceiveMessage);
                    thread.Start(clientSocket);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void ReceiveMessage(object socket)
        {
            Socket clientSocket = (Socket)socket;

            Task.Factory.StartNew((obj) =>
            {
                while (true)
                {
                    var input = Console.ReadLine();
                    var client = (Socket)obj;
                    client.Send(Encoding.UTF8.GetBytes(input));
                }

            }, clientSocket);

            while (true)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    var length = clientSocket.Receive(buffer);

                    Console.WriteLine($"客户端发送的消息:{Encoding.UTF8.GetString(buffer, 0, length)}");
                }
                catch (Exception ex)
                {
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                    break;
                }

            }
        }
    }
}
