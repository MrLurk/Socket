using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Process();
        }

        public static void Process()
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint port = new IPEndPoint(ip, 2020);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(port);
            socket.Listen(0);

            Console.WriteLine("监听已经打开,监听 127.0.0.1:2020");
            var socket1 = socket.Accept();
            Console.WriteLine("连接已经建立");

            while (true)
            {
                string recStr = "";
                byte[] recByte = new byte[4096];
                int bytes = socket1.Receive(recByte, recByte.Length, 0);
                recStr += Encoding.ASCII.GetString(recByte, 0, bytes);
                Console.WriteLine("服务器获得的信息:" + recStr);

                if (recStr == "stop")
                {
                    socket1.Close();
                    Console.WriteLine("连接关闭");
                    break;
                }
                Console.WriteLine("请输入发送到客户端的信息:");
                var sendStr = Console.ReadLine();
                var sendBytes = Encoding.ASCII.GetBytes(sendStr);
                socket1.Send(sendBytes);
                Console.WriteLine("消息已发送");
            }

        }
    }
}
