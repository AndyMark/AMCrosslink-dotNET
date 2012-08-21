using System;
using System.Collections.Generic;
using System.Text;

namespace com.andymark.crosslink
{
    /// <summary>
    /// Represents a Jaguar motor controller.
    /// </summary>
    public class Jaguar : SpeedController
    {
        private Toucan toucan;
        private int nodeId;

        /// <summary>
        /// Initializes a new instance of the Jaguar class for the specified 2CAN and CAN id.
        /// </summary>
        /// <param name="toucan">The Toucan instance associated with this Jaguar</param>
        /// <param name="nodeId">The CAN id of this Jaguar</param>
        public Jaguar(Toucan toucan, int nodeId)
        {
            this.toucan = toucan;
            this.nodeId = nodeId;
        }

        /// <summary>
        /// Sets the throttle output of the SpeedController.
        /// The throttle value is a floating point number, where
        /// 1 is full forward, -1 is full reverse, and 0 is neutral.
        /// </summary>
        public double Throttle
        {
            set
            {
                value = (value > 1) ? 1 : value;
                value = (value < -1) ? -1 : value;
                toucan.SetJaguar(nodeId, (UInt16) (value * 0x7FFF));
            }
        }
    }

}
