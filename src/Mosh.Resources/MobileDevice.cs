using Moryx.AbstractionLayer.Resources;
using Moryx.Serialization;
using Moryx.Threading;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mosh.Resources
{
    [ResourceRegistration]
    public class MobileDevice : Resource
    {
        [EntrySerialize, ReadOnly(true)]
        public IPStatus Status { get; set; }

        [EntrySerialize, ReadOnly(true)]
        public long Roundtrip { get; set; }

        [EntrySerialize, DataMember]
        [Description("Host name to check for the device on the network")]
        public string DeviceName { get; set; }

        [EntrySerialize, DataMember, DefaultValue(10)]
        public int IntervalSec { get; set; }

        private int _timerId = 0;
        private Ping _ping = new Ping();

        public IParallelOperations ParallelOperations { get; set; }

        protected override void OnStart()
        {
            base.OnStart();

            _timerId = ParallelOperations.ScheduleExecution(CheckForDevice, IntervalSec * 1000, IntervalSec * 1000);
        }

        protected override void OnStop()
        {
            ParallelOperations.StopExecution(_timerId);

            base.OnStop();
        }

        private void CheckForDevice()
        {
            var reply = _ping.Send(DeviceName, 1000);
            Status = reply.Status;
            Roundtrip = Status == IPStatus.Success ? reply.RoundtripTime : -1;
        }
    }
}
