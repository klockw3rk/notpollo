using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NotpolloInterop.Classes.P2P;
using NotpolloInterop.Structs.MythicStructs;
namespace NotpolloInterop.Interfaces
{
    public interface IPeerManager
    {
        Peer AddPeer(PeerInformation info);
        bool Remove(string uuid);
        bool Remove(IPeer peer);
        bool Route(DelegateMessage msg);
    }
}
