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

        internal byte[] GetBuffer()
        {
            return packet.GetBuffer();
        }

        internal void SetPWMValue(int channel, UInt16 value)
        {
            packet.SetPWMValue(channel, value);
        }

        internal void SetSolenoidValue(int channel, bool value)
        {
            packet.SetSolenoidValue(channel, value);
        }

        internal void SetRelayValue(int channel, RelayState state)
        {
            packet.SetRelayState(channel, state);
        }
    }
}
