﻿namespace NotpolloInterop.Interfaces
{
    public interface ICryptographicRoutine
    {
        byte[] Encrypt(byte[] data);
        byte[] Decrypt(byte[] data);
    }
}