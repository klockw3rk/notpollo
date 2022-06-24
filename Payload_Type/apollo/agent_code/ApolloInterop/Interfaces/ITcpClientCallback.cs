using NotpolloInterop.Structs.NotpolloStructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace NotpolloInterop.Interfaces
{
    public interface ITcpClientCallback
    {
        void OnAsyncConnect(TcpClient client, out Object state);
        void OnAsyncDisconnect(TcpClient client, Object state);
        void OnAsyncMessageReceived(TcpClient client, IPCData data, Object state);
    }
}
