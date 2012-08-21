using System;
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
        
        /// <param name="ip">The IP address of the 2CAN.</param>
        public SimpleCrosslinkRobot(IPAddress ip)
        {
            toucan = new Toucan(ip);
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
