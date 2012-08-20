using System;
using System.Collections.Generic;
using System.Text;

namespace com.andymark.crosslink
{
    public class GPIO
    {
        private Toucan toucan;
        int channel;

        public GPIO(Toucan toucan, int channel)
        {
            this.toucan = toucan;
            this.channel = channel;
        }

        public bool Value
        {
            get
            {
                return toucan.GetGPIO(channel);
            }
        }
    }
}
