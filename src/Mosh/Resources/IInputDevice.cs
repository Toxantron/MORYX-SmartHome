using Moryx.AbstractionLayer.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mosh.Resources
{
    /// <summary>
    /// General interface for all inputs, analog and binary, button or switch
    /// </summary>
    public interface IInputDevice : IPublicResource
    {
        /// <summary>
        /// Current/last value of the input, 0/1 represent binary states OFF and ON
        /// </summary>
        /// <value>ON</value>
        double Value { get; }

        /// <summary>
        /// Event raised when the value has changed
        /// </summary>
        event EventHandler ValueChanged;
    }

    /// <summary>
    /// Constant values for input devices
    /// </summary>
    public static class InputDeviceValues
    {
        public const double SinglePush = 1.0;

        public const double DoublePush = 2.0;

        public const double TriplePush = 3.0;

        public const double LongPush = 10.0;

        public static bool IsValue(double value, double reference)
        {
            return Math.Abs(value - reference) <= 0.01;
        }
    }

    /// <summary>
    /// Different type of input devices
    /// </summary>
    public enum InputDeviceType
    {
        Unknown,
        Button,
        Switch,

    }
}
