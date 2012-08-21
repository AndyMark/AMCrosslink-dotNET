using System;
using System.Collections.Generic;
using System.Text;

namespace com.andymark.crosslink
{
    /// <summary>
    /// Represents a relay output on the CANipede Robot Control Module.
    /// </summary>
    public class Relay
    {
        private Canipede canipede;
        private int channel;

        /// <summary>
        /// Initializes a new instance of the Relay class for the specified CANipede and relay channel.
        /// </summary>
        /// <param name="canipede">A reference to a Canipede instance</param>
        /// <param name="channel">The relay channel used (1-4).</param>
        public Relay(Canipede canipede, int channel)
        {
            this.canipede = canipede;
            this.channel = channel;
        }

        /// <summary>
        /// The state of the relay output.
        /// </summary>
        public RelayState State
        {
            set
            {
                canipede.SetRelayValue(channel, value);
            }
        }
    }

    /// <summary>
    /// Represents the state of a relay output.
    /// </summary>
    public enum RelayState
    {
        /// <summary>
        /// The relay is OFF, or braked.  Both the A and B outputs are LOW.
        /// </summary>
        Neutral,
        /// <summary>
        /// Runs a connected motor "forward."  The A output is HIGH and the B output is LOW.
        /// </summary>
        Forward,
        /// <summary>
        /// Runs a connected motor "backward."  The A output is LOW and the B output is HIGH.
        /// </summary>
        Reverse
    }
}
