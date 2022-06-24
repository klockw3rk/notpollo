using NotpolloInterop.Structs.MythicStructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotpolloInterop.Interfaces
{
    public interface IPeer
    {
        bool Start();
        void Stop();
        string GetUUID();
        string GetMythicUUID();
        bool Connected();
        void ProcessMessage(DelegateMessage message);
        bool Finished();
    }
}
