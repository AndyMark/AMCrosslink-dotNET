using System;
using System.Collections.Generic;
using System.Text;

namespace com.andymark.crosslink
{
    /// <summary>
    /// Represents a generic motor controller to facilitate easy switching
    /// between Victors, Jaguars, etc.
    /// </summary>
    public interface SpeedController
    {
        /// <summary>
        /// Sets the throttle output of the SpeedController.
        /// The throttle value is a floating point number, where
        /// 1 is full forward, -1 is full reverse, and 0 is neutral.
        /// </summary>
        double Throttle
        {
            set;
        }
    }
}
