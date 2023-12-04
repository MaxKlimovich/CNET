using System.Net.Sockets;
using System.Net;
using System.Text;
using CNET.Server;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Server("Max");
        }

        public static void Server(string name)
        {
            UdpClient udpClient = new UdpClient(12345);
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 0);

            Console.WriteLine("Connecting...");

            while (!Console.KeyAvailable)
            {
                byte[] buffer = udpClient.Receive(ref iPEndPoint);
                var messageText = Encoding.UTF8.GetString(buffer);

                ThreadPool.QueueUserWorkItem(obj =>
                {
                    Message message = Message.DeserializeFromJson(messageText);
                    message.Print();

                    byte[] reply = Encoding.UTF8.GetBytes("Message received");
                    udpClient.Send(reply, reply.Length, iPEndPoint);
                });
            }
        }
    }
}