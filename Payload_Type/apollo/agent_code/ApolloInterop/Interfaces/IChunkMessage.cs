using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotpolloInterop.Interfaces
{
    public interface IChunkMessage
    {
        int GetChunkNumber();
        int GetTotalChunks();
        int GetChunkSize();
    }
}
