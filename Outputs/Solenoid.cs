using System;
using System.Collections.Generic;
using System.Text;

namespace com.andymark.crosslink
{
    public class Solenoid
    {
        private Canipede canipede;
        int channel;

        public Solenoid(Canipede canipede, int channel)
        {
            this.canipede = canipede;
            this.channel = channel;
        }

        public Boolean State
        {
            set { canipede.SetSolenoidValue(channel, value); }
        }
    }
}
