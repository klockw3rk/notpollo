using NotpolloInterop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotpolloInterop.Classes.Events
{
    public class MythicMessageEventArgs : EventArgs
    {
        public IMythicMessage Message;

        public MythicMessageEventArgs(IMythicMessage msg) => Message = msg;
    }
}
