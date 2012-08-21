using System;
using System.Collections.Generic;
using System.Text;

namespace com.andymark.crosslink
{
    /// <summary>
    /// Represents a CANipede solenoid output.
    /// </summary>
    public class Solenoid
    {
        private Canipede canipede;
        private int channel;

        /// <summary>
        /// Initializes a new instance of the Solenoid class for the specified CANipede and output channel.
        /// </summary>
        /// <param name="canipede">The CANipede associated with this servo</param>
        /// <param name="channel">The solenoid channel used (1-8).</param>
        public Solenoid(Canipede canipede, int channel)
        {
            this.canipede = canipede;
            this.channel = channel;
        }

        /// <summary>
        /// Sets the state of the solenoid output.  When true, the solenoid
        /// output is on (- terminal is at GND, + terminal is at Vin).  When
        /// false, the solenoid output is off (- terminal is high impedance,
        /// + terminal is at Vin).
        /// </summary>
        public Boolean State
        {
            set { canipede.SetSolenoidValue(channel, value); }
        }
    }
}
