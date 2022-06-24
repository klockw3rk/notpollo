using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NotpolloInterop.Types.Delegates;
using NotpolloInterop.Structs.MythicStructs;
using NotpolloInterop.Enums.NotpolloEnums;

namespace NotpolloInterop.Interfaces
{
    public interface ITaskManager
    {
        string[] GetExecutingTaskIds();
        bool CancelTask(string taskId);

        bool CreateTaskingMessage(OnResponse<TaskingMessage> onResponse);

        bool ProcessMessageResponse(MessageResponse resp);

        void AddTaskResponseToQueue(TaskResponse message);

        void AddDelegateMessageToQueue(DelegateMessage delegateMessage);

        void AddSocksDatagramToQueue(MessageDirection dir, SocksDatagram dg);

        bool LoadTaskModule(byte[] taskAsm, string[] commands);
    }
}
