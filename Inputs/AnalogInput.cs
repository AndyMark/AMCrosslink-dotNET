using System;
using System.Collections.Generic;
using System.Text;

namespace com.andymark.crosslink
{
    public class AnalogInput
    {
        private Toucan toucan;
        int channel;

        public AnalogInput(Toucan toucan, int channel)
        {
            this.toucan = toucan;
            this.channel = channel;
        }

        public UInt16 Value
        {
            get
            {
                return toucan.GetAnalogRaw(channel);
            }
        }
    }
}
