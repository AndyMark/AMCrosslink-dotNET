using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace com.andymark.crosslink
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    internal class CanipedePacket : CrosslinkPacket
    {
        private byte rcmNodeId;
        private byte reserved;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        private UInt16[] pwm;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        private byte[] relay;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        private byte[] solenoid;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        private byte[] led;
        private byte reserved2;
        private UInt16 crc;

        public CanipedePacket()
        {
            sig = 0xaaa6;
            pwm = new UInt16[8];
            relay = new byte[4];
            solenoid = new byte[8];
            led = new byte[3];
            reserved = 0x0;
            reserved2 = 0x0;
        }

        public int NodeId
        {
            get { return rcmNodeId; }
            set { rcmNodeId = (byte)value; }
        }

        public override byte[] GetBuffer()
        {
            crc = 0x0;
            byte[] buffer = base.GetBuffer();
            crc = CRC(buffer);
            return base.GetBuffer();
        }

        public void SetPWMValue(int channel, UInt16 value)
        {
            pwm[channel - 1] = value;
        }

        public void SetSolenoidValue(int channel, bool value)
        {
            solenoid[channel - 1] = (byte) (value ? 1 : 0);
        }

        public void SetRelayState(int channel, RelayState state)
        {
            byte x;
            switch (state)
            {
                case RelayState.Forward:
                    x = 1;
                    break;
                case RelayState.Reverse:
                    x = 2;
                    break;
                case RelayState.Neutral:
                default:
                    x = 0;
                    break;
            }
            relay[channel - 1] = x;
        }
    }
}
