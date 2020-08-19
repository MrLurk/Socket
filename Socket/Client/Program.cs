using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
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
            socket.Connect(port);
            Console.WriteLine("连接已经建立");

            while (true)
            {
                Console.WriteLine("请输入发送到服务器的信息:");
                var sendStr = Console.ReadLine();
                var sendBytes = Encoding.ASCII.GetBytes(sendStr);
                socket.Send(sendBytes);
                Console.WriteLine("消息已发送");


                string recStr = "";
                byte[] recByte = new byte[4096];
                int bytes = socket.Receive(recByte, recByte.Length, 0);
                recStr += Encoding.ASCII.GetString(recByte, 0, bytes);
                Console.WriteLine("收到的信息:" + recStr);

                if (recStr == "stop")
                {
                    socket.Close();
                    Console.WriteLine("连接关闭");
                    break;
                }
            }

        }

    }
}
