using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Timers;

namespace com.andymark.crosslink
{
    public abstract class SimpleCrosslinkRobot
    {
        protected Toucan toucan;
        private Timer timer;
        
        public SimpleCrosslinkRobot(IPAddress ip, int nodeId)
        {
            toucan = new Toucan(ip);
            timer = new Timer(50);
            timer.Elapsed += new ElapsedEventHandler(periodic);
            timer.Enabled = true;
        }

        public State State
        {
            get { return toucan.State; }
            set { toucan.State = value; }
        }

        public void periodic(object source, ElapsedEventArgs e)
        {
            if (State == State.Teleop)
            {
                teleop();
            }
        }

        public abstract void teleop();        
    }
}
