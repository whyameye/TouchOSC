using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.IO;
using System.Windows.Input;


using libSMARTMultiTouch.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging; //BitmapImage
using libSMARTMultiTouch.Controls;
using System.Diagnostics;

namespace Main
{
    class MainCanvas : Canvas
    {
        private static int frameCount = 0;
        private static int[] blobIDs = new int[40]; // defaults to initial value of 0
        private static double[] blobXs = new double[40];
        private static double[] blobYs = new double[40];

        private static SharpOSC.UDPSender oscSender = new SharpOSC.UDPSender("127.0.0.1", 3333);

        public MainCanvas()
        {
            this.Width = SystemParameters.PrimaryScreenWidth;
            this.Height = SystemParameters.PrimaryScreenHeight;
            this.Cursor = Cursors.None;
            TouchArea b = new TouchArea();
            this.Children.Add(b);
            b.TouchDown += new TouchContactEventHandler(TouchAreaTouchDown);
            b.TouchUp += new TouchContactEventHandler(TouchAreaTouchUp);
            b.TouchMove += new TouchContactEventHandler(TouchAreaTouchMove);
            CompositionTarget.Rendering += CompositionTarget_Rendering;
            Debug.WriteLine("Main Canvas Constructor Complete");
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            // send blob coordinates
            int i = 0;
            List<object> blobAlives = new List<object>();
            blobAlives.Add("alive");
            while (blobIDs[i] != 0)
            {
                var message2 = new SharpOSC.OscMessage("/tuio/2Dcur", "set", blobIDs[i], (float) blobXs[i], (float) blobYs[i]);
                oscSender.Send(message2);
                blobAlives.Add(blobIDs[i]);
                i++;
            }

            // send blob IDs that are alive
            object[] blobAliveArray = blobAlives.ToArray();
            var message = new SharpOSC.OscMessage("/tuio/2Dcur", blobAliveArray);
            oscSender.Send(message);

            // send frame #
            message = new SharpOSC.OscMessage("/tuio/2Dcur", "fseq",frameCount);
            oscSender.Send(message);
            frameCount++;
        }

        void TouchAreaTouchDown(object sender, libSMARTMultiTouch.Input.TouchContactEventArgs e)
        {
            TouchArea b = (TouchArea)sender;
            Debug.WriteLine("DOWN id: " + e.TouchContact.ID + "X: " + e.TouchContact.Position.X + " Y: " + e.TouchContact.Position.Y);
            int i = 0;
            while (blobIDs[i] != 0)
                i++;
            blobIDs[i] = e.TouchContact.ID;
            blobXs[i] = e.TouchContact.Position.X / 1920;
            blobYs[i] = e.TouchContact.Position.Y / 1080;
        }

        void TouchAreaTouchMove(object sender, TouchContactEventArgs e)
        {
            int i = 0;
            while (blobIDs[i] != e.TouchContact.ID)
                i++;
            blobIDs[i] = e.TouchContact.ID;
            blobXs[i] = e.TouchContact.Position.X / 1920;
            blobYs[i] = e.TouchContact.Position.Y / 1080;
        }

        void TouchAreaTouchUp(object sender, libSMARTMultiTouch.Input.TouchContactEventArgs e)
        {
            Debug.WriteLine("UP id: " + e.TouchContact.ID + "X: " + e.TouchContact.Position.X + " Y: " + e.TouchContact.Position.Y);
            int i = 0;
            while (blobIDs[i] != e.TouchContact.ID)
                i++;
            int j = i + 1;
            while (blobIDs[j] != 0)
                j++;
            j--;
            if (j == i)
                blobIDs[i] = 0;
            else
            {
                blobIDs[i] = blobIDs[j];
                blobXs[i] = blobXs[j];
                blobYs[i] = blobYs[j];
                blobIDs[j] = 0;
            }
        }
    }
}