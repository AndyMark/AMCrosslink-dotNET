using System;
using System.Collections.Generic;
using System.Text;

namespace com.andymark.crosslink
{
    public class Relay
    {
        private Canipede canipede;
        int channel;

        public Relay(Canipede canipede, int channel)
        {
            this.canipede = canipede;
            this.channel = channel;
        }

        public RelayState State
        {
            set { }
        }
    }

    public enum RelayState
    {
        Neutral,
        Forward,
        Reverse
    }
}
