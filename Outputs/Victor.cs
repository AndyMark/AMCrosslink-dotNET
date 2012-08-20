using System;
using System.Collections.Generic;
using System.Text;

namespace com.andymark.crosslink
{
    public class Victor : SpeedController
    {
        private Canipede canipede;
        int pwmChannel;

        public Victor(Canipede canipede, int pwmChannel)
        {
            this.canipede = canipede;
            this.pwmChannel = pwmChannel;
            Throttle = 0;
        }

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
