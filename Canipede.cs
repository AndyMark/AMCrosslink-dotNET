using System;
using System.Collections.Generic;
using System.Text;

namespace com.andymark.crosslink
{
    /// <summary>
    /// Represents a CANipede robot control module.
    /// </summary>
    public class Canipede
    {
        private CanipedePacket packet;

        internal Canipede(int id)
        {
            packet = new CanipedePacket();
            packet.NodeId = id;
        }

        public void SetNodeId(int id)
        {
            packet.NodeId = id;
        }

        public byte[] GetBuffer()
        {
            return packet.GetBuffer();
        }

        public void SetPWMValue(int channel, UInt16 value)
        {
            packet.SetPWMValue(channel, value);
        }

        public void SetSolenoidValue(int channel, bool value)
        {
            packet.SetSolenoidValue(channel, value);
        }

        public void SetRelayValue(int channel, RelayState state)
        {
            packet.SetRelayState(channel, state);
        }
    }
}
