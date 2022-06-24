using NotpolloInterop.Interfaces;
using NotpolloInterop.Structs.NotpolloStructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KeylogInject
{
    public static class Delegates
    {
        public delegate bool PushKeylog(IMythicMessage info);
    }
}
