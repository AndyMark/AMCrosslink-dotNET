using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace com.andymark.crosslink
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    internal abstract class CrosslinkPacket
    {
        protected UInt16 sig;
        protected UInt16 len;

        protected CrosslinkPacket()
        {
            len = (UInt16) Marshal.SizeOf(this);
        }

        public virtual byte[] GetBuffer()
        {
            byte[] buffer = new byte[len];
            IntPtr ptr = Marshal.AllocHGlobal(len);
            Marshal.StructureToPtr(this, ptr, true);
            Marshal.Copy(ptr, buffer, 0, len);
            Marshal.FreeHGlobal(ptr);
            return buffer;
        }

        public static UInt16 CRC(byte[] buffer)
        {
            UInt16 sum = 0;
            
            for (int i = 0; i < buffer.Length ; i += 2)
            {
                sum += BitConverter.ToUInt16(buffer, i);
            }
            sum = (UInt16) ((~sum + 1) & 0xffff);
            return sum;
        }
    }
}
