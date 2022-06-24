﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AI = NotpolloInterop;
using NotpolloInterop.Interfaces;
using NotpolloInterop.Structs.MythicStructs;
using System.Collections.Concurrent;
using NotpolloInterop.Classes;
using System.Net.Sockets;
using NotpolloInterop.Structs.NotpolloStructs;
using NotpolloInterop.Enums.NotpolloEnums;
using static NotpolloInterop.Structs.MythicStructs.MessageResponse;
using System.Net;

namespace Notpollo.Management.Socks
{
    public class SocksManager : AI.Classes.SocksManager
    {
        private ConcurrentDictionary<int, SocksClient> _connections = new ConcurrentDictionary<int, SocksClient>();
        
        public SocksManager(IAgent agent) : base(agent)
        {

        }

        public override bool Route(SocksDatagram dg)
        {
            if (!_connections.ContainsKey(dg.ServerID))
            {
                if (!dg.Exit)
                {
                    SocksClient c = new SocksClient(_agent, dg.ServerID);
                    _connections.AddOrUpdate(c.ID, c, (int i, SocksClient d) => { return d; });
                } else { return dg.Exit; }
            }
            if (dg.Exit)
            {
                _connections[dg.ServerID].Exit();
                return dg.Exit;
            }
            return _connections[dg.ServerID].HandleDatagram(dg);
        }

        public override bool Remove(int id)
        {
            _connections[id].Exit();
            return _connections.TryRemove(id, out SocksClient _);
        }
    }
}
