using System.Net;
using System.Net.Sockets;
using System.Text;
using CNET.Server;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            SentMessage("Server");
        }

        public static void SentMessage(string From, string ip = "127.0.0.1")
        {
            UdpClient udpClient = new UdpClient();
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), 12345);

            while (true)
            {
                Console.Write("Enter message (or 'Exit' for exit): ");
                string input = Console.ReadLine();

                if (input.ToLower() == "exit")
                {
                    break;
                }

                Message message = new Message() { Text = input, DateTime = DateTime.Now, NicknameFrom = From, NicknameTo = "All" };
                string json = message.SerializemessageToJson();

                byte[] data = Encoding.UTF8.GetBytes(json);
                udpClient.Send(data, data.Length, iPEndPoint);

                byte[] buffer = udpClient.Receive(ref iPEndPoint);
                var answer = Encoding.UTF8.GetString(buffer);
                Console.WriteLine(answer);
            }
        }
    }
}