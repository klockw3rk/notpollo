using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotpolloInterop.Interfaces
{
    public interface IEncryptedFileStore
    {
        bool TryAddOrUpdate(string keyName, byte[] data);

        bool TryGetValue(string keyName, out byte[] data);

        string GetScript();
        void SetScript(string script);
        void SetScript(byte[] script);
    }
}
