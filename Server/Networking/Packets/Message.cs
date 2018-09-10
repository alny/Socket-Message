using Client.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Networking.Packets {
    public class Message : PacketStructure {

        private string _message;

        public Message(string message) 
            : base((ushort)(4 + message.Length), 2000) {
            Text = message;
            //ushort msgLength = (ushort)_message.Length;
            //RewriteHeader((ushort)(4 + msgLength), 2000);
        }

        public Message(byte[] packet) :base(packet) {

        }


        public string Text {
            get { return ReadString(4, Data.Length - 4); }
            set {
                _message = value;
                WriteUString(value, 4);
            }
        }

    }
}
