using System;
using System.ComponentModel;
using Moryx.Runtime.Modules;
using Moryx.Serialization;

namespace Mosh.Adapters.GridFrequency
{
    [ServerModuleConsole]
    public class ModuleConsole : IServerModuleConsole
    {
        public void ExecuteCommand(string[] args, Action<string> outputStream)
        {
            if (args.Length < 2)
            {
                outputStream("Insufficient arguments!");
                return;
            }

            if (args[0] == "hello")
                outputStream(SayHello(args[1]));
        }

        [EntrySerialize]
        public string SayHello(string name)
        {
            return $"Hello {name}";
        }
    }
}