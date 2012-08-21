﻿using System;
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
        private IPEndPoint tx_dest;
        private Socket rx_socket;
        private byte[] rx_buffer;
        private JaguarPacket jaguarPacket;
        private EnablePacket enablePacket;
        private StatusPacket statusPacket;
        private Dictionary<int, Canipede> canipedes;

        /// <summary>
        /// Initializes a new instance of the Toucan class.
        /// </summary>
        /// <param name="addr">The IP address of the 2CAN device.</param>
        public Toucan(IPAddress addr)
        {
            canipedes = new Dictionary<int, Canipede>();
            jaguarPacket = new JaguarPacket();
            enablePacket = new EnablePacket();
            statusPacket = new StatusPacket();

            tx_dest = new IPEndPoint(addr, 1217);

            rx_buffer = new byte[62];
            rx_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            rx_socket.ReceiveTimeout = 1000;
            rx_socket.Bind(new IPEndPoint(IPAddress.Any, 1218));
            rx_socket.BeginReceive(rx_buffer, 0, rx_buffer.Length, SocketFlags.None, ReceivePacket, null);

            tx_timer = new System.Timers.Timer(50);
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

        private void ReceivePacket(IAsyncResult ar)
        {
            statusPacket = StatusPacket.ParseFromBuffer(rx_buffer);
            rx_socket.BeginReceive(rx_buffer, 0, rx_buffer.Length, SocketFlags.None, ReceivePacket, null);
        }

        /// <summary>
        /// Gets or sets the state of the control system.
        /// </summary>
        public State State
        {
            get { return enablePacket.State; }
            set { enablePacket.State = value; }
        }

        /// <summary>
        /// Gets the Canipede instance associated with the specified CAN ID.
        /// </summary>
        /// <param name="nodeId">The CAN ID where the CANipede is located</param>
        /// <returns>A Canipede instance</returns>
        public Canipede GetCanipede(int nodeId)
        {
            if (!canipedes.ContainsKey(nodeId))
            {
                Canipede canipede = new Canipede(nodeId);
                canipedes.Add(nodeId, canipede);
            }
            return canipedes[nodeId];
        }

        internal void SetJaguar(int jaguar, UInt16 value)
        {
            jaguarPacket.SetJaguarValue(jaguar, value);
        }

        internal UInt16 GetAnalogRaw(int channel)
        {
            return statusPacket.analog_in[channel - 1];
        }

        internal bool GetGPIO(int channel)
        {
            byte bitmask = (byte) (0x1 << (channel - 1));
            return (statusPacket.gpio_in & bitmask) != 0;
        }

        internal int GetEncoderPosition(int encoderChannel)
        {
            return statusPacket.quad_in[encoderChannel - 1];
        }

        internal int GetEncoderRate(int encoderChannel)
        {
            return statusPacket.velocity_in[encoderChannel - 1];
        }
    }

    /// <summary>
    /// Represents the state of a robot.
    /// </summary>
    public enum State
    {
        /// <summary>
        /// Robot is disabled.  All outputs should be neutral.
        /// </summary>
        Disabled,
        /// <summary>
        /// Robot is enabled for remote control.  Outputs are enabled.
        /// </summary>
        Teleop
    }
}
