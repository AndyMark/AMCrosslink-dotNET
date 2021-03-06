﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Timers;

namespace com.andymark.crosslink
{
    /// <summary>
    /// A simple robot base class.
    /// </summary>
    /// You can build a robot program from this class by overriding the
    /// teleop() and disabled() methods.
    public abstract class SimpleCrosslinkRobot
    {
        /// <summary>
        /// The Toucan associated with this robot.
        /// </summary>
        protected Toucan toucan;
        private Timer timer;
        private AnalogInput battery;
        
        /// <param name="ip">The IP address of the 2CAN.</param>
        public SimpleCrosslinkRobot(IPAddress ip)
        {
            toucan = new Toucan(ip);
            battery = new AnalogInput(toucan, 8);

            timer = new Timer(50);
            timer.Elapsed += new ElapsedEventHandler(periodic);
            timer.Enabled = true;
        }

        /// <summary>
        /// Gets or sets the state of the robot.
        /// </summary>
        public State State
        {
            get { return toucan.State; }
            set { toucan.State = value; }
        }

        /// <summary>
        /// Changes the Toucan IP Address.
        /// </summary>
        /// <param name="addr">New IP Address to use</param>
        public void ChangeIPAddress(IPAddress addr)
        {
            toucan.ChangeIPAddress(addr);
        }

        /// <summary>
        /// True if we've received a packet from the robot in the last 100 milliseconds.
        /// </summary>
        public Boolean ReceivingPackets
        {
            get
            {
                return toucan.TimeSinceLastRx.TotalMilliseconds < 100;
            }
        }

        /// <summary>
        /// Gets the battery voltage (from analog input 8) when the jumper
        /// is in place on the CANipede.
        /// </summary>
        /// <returns></returns>
        public double GetBatteryVoltage()
        {
            return battery.BatteryVoltage;
        }

        private void periodic(object source, ElapsedEventArgs e)
        {
            switch (State)
            {
                case crosslink.State.Disabled:
                    disabled();
                    break;
                case crosslink.State.Teleop:
                    teleop();
                    break;
            }
        }

        /// <summary>
        /// Called when the robot is in the Teleop state.
        /// </summary>
        public virtual void teleop()
        {
        }

        /// <summary>
        /// Called when the robot is in the Disabled state.
        /// </summary>
        public virtual void disabled()
        {
        }
    }
}
