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
        }

        void TouchAreaTouchDown(object sender, libSMARTMultiTouch.Input.TouchContactEventArgs e)
        {
            TouchArea b = (TouchArea)sender;
            Debug.WriteLine("DOWN id: " + e.TouchContact.ID + "X: " + e.TouchContact.Position.X + " Y: " + e.TouchContact.Position.Y);
        }

        void TouchAreaTouchMove(object sender, TouchContactEventArgs e)
        {
            //Debug.WriteLine("MOVE id: " + e.TouchContact.ID + "X: " + e.TouchContact.Position.X + " Y: " + e.TouchContact.Position.Y);
        }

        void TouchAreaTouchUp(object sender, libSMARTMultiTouch.Input.TouchContactEventArgs e)
        {
            Debug.WriteLine("UP id: " + e.TouchContact.ID + "X: " + e.TouchContact.Position.X + " Y: " + e.TouchContact.Position.Y);
        }
    }
}