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

        public double Revolutions
        {
            get
            {
                return toucan.GetEncoderPosition(channel);
            }
        }

        public double Velocity
        {
            get
            {
                return toucan.GetEncoderPosition(channel);
            }
        }
    }
}
