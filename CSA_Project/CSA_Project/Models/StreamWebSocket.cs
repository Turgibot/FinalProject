
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.WebSockets;
using System.Web.Script.Serialization;

namespace CSA_Project.Models
{
    public class StreamWebSocket : WebSocketHandler
    {
        private string user;

        private JavaScriptSerializer serializer = new JavaScriptSerializer();
        private static WebSocketCollection socketCollection = new WebSocketCollection();

        public StreamWebSocket(string user)
        {
            this.user = user;
        }

        public override void OnClose()
        {
            base.OnClose();
        }

        public override void OnError()
        {
            base.OnError();
        }

        public override void OnMessage(string message)
        {
            base.OnMessage(message);
        }

        public override void OnMessage(byte[] message)
        {
            base.OnMessage(message);
        }

        public override void OnOpen()
        {
            base.OnOpen();
            socketCollection.Add(this);
            socketCollection.Broadcast(serializer.Serialize(new
            {
                type = MessageType.Join,
                from = user,
                value = user+" joined the server."
            }));

        }

        enum MessageType { Join, Quit}

        
    }
}