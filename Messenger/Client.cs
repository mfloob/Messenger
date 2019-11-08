using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Messenger
{
    public class Client
    {
        private IPAddress _ip { get; set; }
        private IPEndPoint _endPoint { get; set; }
        private Socket _sender { get; set; }

        public Client(int port)
        {
            _ip = Dns.GetHostEntry("localhost").AddressList[0];
            _endPoint = new IPEndPoint(_ip, port);
            _sender = new Socket(_ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            Connect();
        }

        public void Connect() => _sender.Connect(_endPoint);

        public void SendMessage(string message)
        {
            try
            {
                byte[] msg = Encoding.ASCII.GetBytes(message.TrimEnd());
                int bytesSent = _sender.Send(msg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public string Listen()
        {
            var bytes = new byte[1024];
            var bytesRec = 0;
            var msg = "";

            try
            {
                if (_sender.Poll(10000, SelectMode.SelectRead))
                    bytesRec = _sender.Receive(bytes);
                msg = Encoding.ASCII.GetString(bytes, 0, bytesRec);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return msg;
        }
    }
}
