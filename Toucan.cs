using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Timers;

namespace com.andymark.crosslink
{
    /// <summary>
    /// Represents a 2CAN.
    /// </summary>
    public class Toucan
    {
        private Timer tx_timer;
        private Timer rx_timer;
        private Socket rx_socket;
        private IPEndPoint tx_dest;
        private JaguarPacket jaguarPacket;
        private EnablePacket enablePacket;
        private StatusPacket statusPacket;
        private Dictionary<int, Canipede> canipedes;

        public Toucan(IPAddress addr)
        {
            canipedes = new Dictionary<int, Canipede>();
            jaguarPacket = new JaguarPacket();
            enablePacket = new EnablePacket();
            statusPacket = new StatusPacket();

            tx_dest = new IPEndPoint(addr, 1217);

            rx_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            rx_socket.Bind(new IPEndPoint(IPAddress.Any, 1218));
            
            rx_timer = new Timer(50);
            rx_timer.Elapsed += new ElapsedEventHandler(ReceivePackets);
            rx_timer.Enabled = true;

            tx_timer = new Timer(50);
            tx_timer.Elapsed += new ElapsedEventHandler(SendPackets);
            tx_timer.Enabled = true;
        }

        private void SendPackets(object source, ElapsedEventArgs e)
        {
            UdpClient tx_client;
            try
            {
                tx_client = new UdpClient(1206);
            }
            catch (Exception)
            {
                return;
            }
            
            byte[] arr = enablePacket.GetBuffer();
            tx_client.Send(arr, arr.Length, tx_dest);

            arr = jaguarPacket.GetBuffer();
            tx_client.Send(arr, arr.Length, tx_dest);

            foreach (KeyValuePair<int, Canipede> canipede in canipedes)
            {
                arr = canipede.Value.GetBuffer();
                tx_client.Send(arr, arr.Length, tx_dest);
            }

            tx_client.Close();
        }

        private void ReceivePackets(object source, ElapsedEventArgs e)
        {
            byte[] buffer = new byte[62];
            rx_socket.Receive(buffer);
            statusPacket = StatusPacket.ParseFromBuffer(buffer);
        }

        public State State
        {
            get { return enablePacket.State; }
            set { enablePacket.State = value; }
        }

        public Canipede GetCanipede(int nodeId)
        {
            if (!canipedes.ContainsKey(nodeId))
            {
                Canipede canipede = new Canipede(nodeId);
                canipedes.Add(nodeId, canipede);
            }
            return canipedes[nodeId];
        }

        public void SetJaguar(int jaguar, UInt16 value)
        {
            jaguarPacket.SetJaguarValue(jaguar, value);
        }

        public UInt16 GetAnalogRaw(int channel)
        {
            return statusPacket.analog_in[channel - 1];
        }

        public bool GetGPIO(int channel)
        {
            byte bitmask = (byte) (0x1 << (channel - 1));
            return (statusPacket.gpio_in & bitmask) != 0;
        }

        public int GetEncoderPosition(int encoderChannel)
        {
            return statusPacket.quad_in[encoderChannel - 1];
        }

        public int GetEncoderVelocity(int encoderChannel)
        {
            return statusPacket.velocity_in[encoderChannel - 1];
        }
    }

    public enum State
    {
        Disabled,
        Teleop,
        Auto
    }
}
