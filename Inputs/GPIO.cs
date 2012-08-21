using System;
using System.Collections.Generic;
using System.Text;

namespace com.andymark.crosslink
{
    /// <summary>
    /// Represents a GPIO Input on the CANipede Robot Control Module.
    /// 
    /// The present CTRE firmware only supports using this as an input.
    /// </summary>
    public class GPIO
    {
        private Toucan toucan;
        private int channel;

        /// <summary>
        /// Initializes a new instance of the GPIO class for the specified 2CAN and GPIO channel.
        /// </summary>
        /// <param name="toucan">The 2CAN instance associated with this digital input</param>
        /// <param name="channel">The GPIO channel used (1-4).</param>
        public GPIO(Toucan toucan, int channel)
        {
            this.toucan = toucan;
            this.channel = channel;
        }

        /// <summary>
        /// The state of the digital input;  TRUE if the signal pin is high
        /// or FALSE if the signal pin is near ground.  Note that the
        /// signal pin is pulled high when there is no connection.
        /// </summary>
        public bool Value
        {
            get
            {
                return toucan.GetGPIO(channel);
            }
        }
    }
}
