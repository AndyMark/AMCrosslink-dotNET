using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace com.andymark.crosslink
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    internal class StatusPacket : CrosslinkPacket
    {
        internal byte gpio_ddr;
		internal byte gpio_out;
		internal byte gpio_in;
		internal byte gpio_pue;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		internal Int32[] quad_in;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		internal Int32[] velocity_in;
		
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		internal UInt16[] analog_in;
		
		internal UInt32 flags;

        public StatusPacket()
        {
            sig = 0xaaa7;
            quad_in = new Int32[4];
            velocity_in = new Int32[4];
            analog_in = new UInt16[8];
        }

        public static StatusPacket ParseFromBuffer(byte[] buffer)
        {
            StatusPacket packet = new StatusPacket();
            int size = Marshal.SizeOf(packet);
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.Copy(buffer, 0, ptr, size);

            packet = (StatusPacket)Marshal.PtrToStructure(ptr, packet.GetType());
            Marshal.FreeHGlobal(ptr);

            return packet;
        }
    }
}
