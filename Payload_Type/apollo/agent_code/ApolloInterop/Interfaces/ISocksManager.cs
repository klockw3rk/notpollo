using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NotpolloInterop.Structs.MythicStructs;

namespace NotpolloInterop.Interfaces
{
    public interface ISocksManager
    {
        bool Route(SocksDatagram dg);

        bool Remove(int id);
    }
}
