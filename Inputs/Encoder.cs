using System;
using System.Collections.Generic;
using System.Text;

namespace com.andymark.crosslink
{
    /// <summary>
    /// Represents a quadrature encoder connected to the CANipede robot control module.
    /// 
    /// These inputs are marked "QUAD" on the CANipede.
    /// </summary>
    public class Encoder
    {
        private Toucan toucan;
        private int channel;

        /// <summary>
        /// Initializes a new instance of the Encoder class for the specified 2CAN and input channel.
        /// </summary>
        /// <param name="toucan">The Toucan instance associated with this Encoder</param>
        /// <param name="channel">The QUAD input in use (1-4).</param>
        public Encoder(Toucan toucan, int channel)
        {
            this.toucan = toucan;
            this.channel = channel;
        }

        /// <summary>
        /// Gets the total distance traveled since start-up.
        /// There are two counts per cycle, so for a 250 cycles/rev
        /// encoder, there will be 500 counts/revolution here.
        /// </summary>
        public int Position
        {
            get
            {
                return toucan.GetEncoderPosition(channel);
            }
        }

        /// <summary>
        /// Gets the current encoder rate, in counts / 100 ms.
        /// </summary>
        public int Rate
        {
            get
            {
                return toucan.GetEncoderRate(channel);
            }
        }
    }
}
