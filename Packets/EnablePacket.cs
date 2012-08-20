using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace com.andymark.crosslink
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    internal class EnablePacket : CrosslinkPacket
    {
        private byte enable;
        private byte reserved;
        private ulong oe;
        private UInt16 seq;
        private UInt16 crc;

        public EnablePacket()
        {
            sig = 0xaaac;
            oe = 0xffffffffffffffff;
        }

        public State State
        {
            get
            {
                switch (enable)
                {
                    case 0x0:
                        return State.Disabled;
                    case 0x1:
                        return State.Teleop;
                    default:
                        enable = 0x0;
                        return State.Disabled;
                }
            }

            set
            {
                switch (value)
                {
                    case State.Disabled:
                        enable = 0x0;
                        break;
                    case State.Teleop:
                        enable = 0x1;
                        break;
                }
            }
        }

        public override byte[] GetBuffer()
        {
            seq++;
            crc = 0x0;
            byte[] buffer = base.GetBuffer();
            crc = CRC(buffer);
            return base.GetBuffer();
        }
    }
}
