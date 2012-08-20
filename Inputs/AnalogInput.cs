using System;
using System.Collections.Generic;
using System.Text;

namespace com.andymark.crosslink
{
    /// <summary>
    /// Represents an analog input on the Canipede.
    /// </summary>
    public class AnalogInput
    {
        private Toucan toucan;
        int channel;

        public AnalogInput(Toucan toucan, int channel)
        {
            this.toucan = toucan;
            this.channel = channel;
        }

        /// <summary>
        /// Raw data received from the Canipede.
        /// </summary>
        public UInt16 Raw
        {
            get
            {
                return toucan.GetAnalogRaw(channel);
            }
        }

        /// <summary>
        /// Value expressed as a voltage.
        /// </summary>
        public double Voltage
        {
            get
            {
                return ((int)Raw) * 0.0048984375; // raw * 3.3/1024 * ( 5.2 + 10) /  (10)
            }
        }

        /// <summary>
        /// Canipede voltage on analog input 8 when the jumper is in place.
        /// Compensates for the internal voltage divider.
        /// </summary>
        public double BatteryVoltage
        {
            get
            {
                return ((int)Raw) * 0.02745703125; // raw * 3.3/1024 * ( 70 + 5.2 + 10) /  (10)
            }
        }
    }
}
