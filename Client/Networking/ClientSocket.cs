using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client.Networking {

    

    public class ClientSocket {

        private Socket _socket;
        private byte[] _buffer = new byte[1024];

        public ClientSocket() {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connect(string ipAddress, int port) {
            _socket.BeginConnect(new IPEndPoint(IPAddress.Parse(ipAddress), port), ConnectCallback, null);
        }

        private void ConnectCallback(IAsyncResult result) {

            if(_socket.Connected) {
                Console.WriteLine("Connected to Server");
                byte[] buffer = new byte[1024];
                _socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceivedCallback, null);

                #region Initial Packet

                byte[] packet = new byte[4];

                byte[] packetLength = BitConverter.GetBytes((ushort)packet.Length);
                byte[] packetType = BitConverter.GetBytes((ushort)1000);

                Array.Copy(packetLength, packet, 2);
                Array.Copy(packetType, 0, packet, 2, 2);
                _socket.Send(packet);

                #endregion


            } else Console.WriteLine("Could not connect");
        }

        private void ReceivedCallback(IAsyncResult result) {

            int bufLength = _socket.EndReceive(result);
            byte[] packet = new byte[bufLength];
            Array.Copy(_buffer, packet, packet.Length);

            //Handle the packet
            byte[] buffer = new byte[1024];
            _socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceivedCallback, null);


        }

        public void Send(byte[] data) {
            _socket.Send(data);
        }



    }
}
