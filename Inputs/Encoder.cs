using System;
using System.Collections.Generic;
using System.Text;

namespace com.andymark.crosslink
{
    public class Encoder
    {
        private Toucan toucan;
        int channel;

        public Encoder(Toucan toucan, int channel)
        {
            this.toucan = toucan;
            this.channel = channel;
        }

        public double Position
        {
            get
            {
                return toucan.GetEncoderPosition(channel);
            }
        }

        public double Rate
        {
            get
            {
                return toucan.GetEncoderRate(channel);
            }
        }
    }
}
