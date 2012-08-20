using System;
using System.Collections.Generic;
using System.Text;

namespace com.andymark.crosslink
{
    public class Jaguar : SpeedController
    {
        private Toucan toucan;
        int nodeId;

        public Jaguar(Toucan toucan, int nodeId)
        {
            this.toucan = toucan;
            this.nodeId = nodeId;
        }

        public double Throttle
        {
            set
            {
                value = (value > 1) ? 1 : value;
                value = (value < -1) ? -1 : value;
                toucan.SetJaguar(nodeId, (UInt16) (value * 0x7FFF));
            }
        }
    }

}
