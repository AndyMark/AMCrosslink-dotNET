/*! \mainpage
 *
 * \section intro_sec Introduction
 *
 * This is a .NET library for use with the Crosslink Robot Control System.
 * The source code is licensed under a BSD-style open source
 * license.
 * 
 * Click <a href="http://www.andymark.com/product-p/am-0994.htm">here</a> to purchase a Crosslink control system.
 *
 * \section download_sec Downloads
 *
 * <a href="AMCrosslink-dotNet-v1.0.zip">AMCrosslink-dotNet-v1.0.zip</a>
 *
 * \section source_sec Source Code
 *
 * The source code for the library is available on <a href="https://github.com/AndyMark/AMCrosslink-dotNET">GitHub</a>.
 *
 * \section gettingStarted_sec Getting Started
 *
 * <ol>
 * <li>Download the Zip archive and decompress it somewhere convenient.</li>
 * <li>Create a new Visual Studio project and add a reference to the DLL.</li>
 * <li>Subclass \ref com.andymark.crosslink.SimpleCrosslinkRobot "SimpleCrosslinkRobot" by overriding \ref com.andymark.crosslink.SimpleCrosslinkRobot.teleop() "teleop()" and \ref com.andymark.crosslink.SimpleCrosslinkRobot.disabled() "disabled()" with appropiate code for your robot:</li>
\code{.cs}
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using com.andymark.crosslink;

namespace org.mynamespace
{
    class MyRobot : SimpleCrosslinkRobot
    {
        private SpeedController leftFront;
        private SpeedController rightFront;
        private SpeedController leftRear;
        private SpeedController rightRear;

        public MyRobot(IPAddress ip) : base(ip)
        {
            Canipede canipede = toucan.GetCanipede(1);

            leftFront = new Jaguar(toucan, 2);
            rightFront = new Jaguar(toucan, 3);
            leftRear = new Jaguar(toucan, 4);
            rightRear = new Jaguar(toucan, 5);
        }

        public override void teleop()
        {
            drive(0, 0, 0.3);  // spin clockwise at 30% throttle
        }

        private void drive(double x, double y, double rotate)
        {
            double lf = x + y + rotate;
            double rf = x - y + rotate;
            double lr = -x + y + rotate;
            double rr = -x - y + rotate;

            // normalize and scale power
            double max = Math.Max(Math.Max(lf, rf), Math.Max(lr, rr));
            if (max > 1.0)
            {
                lf /= max;
                rf /= max;
                lr /= max;
                rr /= max;
            }
            leftFront.Throttle = lf * driveGain / 10;
            rightFront.Throttle = rf * driveGain / 10;
            leftRear.Throttle = lr * driveGain / 10;
            rightRear.Throttle = rr * driveGain / 10;
        }
    }
}
\endcode
 * </ol>
 */
