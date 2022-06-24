﻿using NotpolloInterop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotpolloInterop.Classes.Events
{
    public class ChunkMessageEventArgs<T> : EventArgs where T : IChunkMessage
    {
        public T[] Chunks;

        public ChunkMessageEventArgs(T[] chunks)
        {
            Chunks = chunks;
        }
    }
}
