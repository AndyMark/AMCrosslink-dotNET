using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace com.andymark.crosslink
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    internal class JaguarPacket : CrosslinkPacket
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        private UInt16[] mode;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        private UInt16[] setVoltage;
        private UInt16 crc;

        public JaguarPacket()
        {
            sig = 0xaaa0;
            mode = new UInt16[20];
            setVoltage = new UInt16[20];
        }

        public override byte[] GetBuffer()
        {
            crc = 0x0;
            byte[] buffer = base.GetBuffer();
            crc = CRC(buffer);
            return base.GetBuffer();
        }

        public void SetJaguarValue(int output, UInt16 value)
        {
            mode[output - 1] = 0x00;
            setVoltage[output - 1] = value;
        }
    }
}
