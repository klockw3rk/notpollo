﻿using NotpolloInterop.Structs.NotpolloStructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace NotpolloInterop.Classes
{
    public class TcpMessageEventArgs : EventArgs
    {
        public TcpClient Client;
        public IPCData Data;
        public Object State;

        public TcpMessageEventArgs(TcpClient client, IPCData? data, Object state)
        {
            Client = client;
            if (data != null)
                Data = (IPCData)data;
            State = state;
        }
    }
}
