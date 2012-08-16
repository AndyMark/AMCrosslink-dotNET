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
        
        public SimpleCrosslinkRobot(String ip, int nodeId)
        {
            toucan = new Toucan(IPAddress.Parse(ip));
            timer = new Timer(50);
            timer.Elapsed += new ElapsedEventHandler(periodic);
            timer.Enabled = true;
        }

        public double getBatteryVoltage()
        {
            uint raw = 0;
            // TODO DLL.CTR_GetADC(dllHandle, DLL.Node.RCM, 1, 7, ref raw);
            double dVal = ((int) raw) * 0.02745703125; // * 3.3/1024 * ( 70 + 5.2 + 10) /  ( 10)
            return dVal;
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
