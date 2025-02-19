using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//manual imports
using libSMARTMultiTouch.Controls; //DraggableBorder
using System.Windows.Media.Imaging; //BitmapImage
using System.Windows.Media; //ImageBrush
using System.Windows; //Point,
using System.Windows.Media.Effects; 
using System.Windows.Controls; //Canvas.setTop.....
using System.Diagnostics;

using System.Windows.Media.Animation;
using System.IO;


namespace Main
{
    class TouchArea :  InteractiveBorder
    {
        public TouchArea()
        {
            this.MinHeight = 300;
            this.MinWidth = 300;

            this.MinHeight = SystemParameters.PrimaryScreenHeight;
            this.MinWidth = SystemParameters.PrimaryScreenWidth;

            this.Background = new SolidColorBrush(Colors.Red);
            //this.Foreground = new SolidColorBrush(Colors.Red);
            this.Opacity = 1;
            Debug.WriteLine("building constructor");
        }       
    }
}

