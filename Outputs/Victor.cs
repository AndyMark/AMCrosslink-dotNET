using System;
using System.Collections.Generic;
using System.Text;

namespace com.andymark.crosslink
{
    /// <summary>
    /// Represents a Victor 883/884 motor controller.
    /// </summary>
    public class Victor : SpeedController
    {
        private Canipede canipede;
        private int pwmChannel;

        /// <summary>
        /// Initializes a new instance of the Victor class for the specified CANipede and PWM channel.
        /// </summary>
        /// <param name="canipede">The CANipede instance associated with this Victor.</param>
        /// <param name="pwmChannel">The PWM output channel for this Victor (1-8).</param>
        public Victor(Canipede canipede, int pwmChannel)
        {
            this.canipede = canipede;
            this.pwmChannel = pwmChannel;
            Throttle = 0;
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
                double forward = 2.1;
                double center = 1.50;
                double reverse = 1.00;
                double ms;

                value = (value > 1) ? 1 : value;
                value = (value < -1) ? -1 : value;

                if (value > 0)
                {
                    ms = center + value * (forward - center);
                }
                else
                {
                    ms = center + value * (center - reverse);
                }

                canipede.SetPWMValue(pwmChannel, (UInt16)(ms * 1e6 / 200));
            }
        }
    }
}
