using Moryx.Communication;
using Mosh.Protocols.Shelly;
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

    /// <summary>
    /// Command message for TOPIC_PREFIX/command/{SwitchTopic}
    /// </summary>
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

        public string? Prefix { get; set; }

        public int Switch { get; set; }

        public string SwitchTopic
        {
            get => $"switch:{Switch}";
            set => Switch = int.Parse(value.Split(':')[1]);
        }

        public ShellyCommand Command { get; private set; }

        public int ToggleBack { get; set; }

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