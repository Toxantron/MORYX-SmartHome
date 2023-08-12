using Moryx.Communication;
using System.Text;

namespace Moryx.Protocols.Shelly
{
    public enum ShellyCommand
    {
        On,
        Off,
        Toggle,
        Status_Update
    }
    public class ShellyCommandMessage : IByteSerializable
    {
        public ShellyCommandMessage()
        {
        }

        public ShellyCommandMessage(string prefix, ShellyCommand command)
        {
            Prefix = prefix;
            Command = command;
        }

        public string Prefix { get; private set; }

        public ShellyCommand Command { get; private set; }

        public int Switch { get; set; }

        public int ToggleBack { get; set; }

        public string SwitchTopic => $"switch:{Switch}";

        public void FromBytes(byte[] bytes)
        {
            
        }

        public byte[] ToBytes()
        {
            string command = Command.ToString("G").ToLower();

            if (ToggleBack > 0)
                command += $",{ToggleBack}";

            return Encoding.UTF8.GetBytes(command);
        }
    }
}