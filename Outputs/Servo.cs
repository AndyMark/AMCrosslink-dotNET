using System;
using System.Collections.Generic;
using System.Text;

namespace com.andymark.crosslink
{
    /// <summary>
    /// Represents a "hobby" servo connected to a CANipede PWM output.
    /// </summary>
    public class Servo
    {
        private Canipede canipede;
        private int pwmChannel;

        /// <summary>
        /// Initializes a new instance of the Servo class for the specified CANipede and PWM channel.
        /// </summary>
        /// <param name="canipede">The CANipede associated with this servo.</param>
        /// <param name="pwmChannel">The channel of the PWM output the servo is connected to (1-8).</param>
        public Servo(Canipede canipede, int pwmChannel)
        {
            this.canipede = canipede;
            this.pwmChannel = pwmChannel;
        }

        /// <summary>
        /// Sets the position of the servo, represented as a floating point number from 1 to -1.
        /// </summary>
        public double Position
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
