//main program for app

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using libSMARTMultiTouch.Controls;
using Main;
using System.Diagnostics;


namespace Main
{
    /// <summary>
    /// Interaction logic for TableControl.xaml
    /// </summary>
    public partial class TableControl : TableApplicationControl
    {

        public TableControl()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void TableApplicationControl_Loaded(object sender, RoutedEventArgs e)
        {
            
            MainCanvas mapCanvas = new MainCanvas();
            TableLayoutRoot.Children.Add(mapCanvas);
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // After the window loads, search the entire visual tree for the CornerCloseButton
            CornerCloseButton theButtonToDisable = FindChildOfType<CornerCloseButton>(this);

            if (theButtonToDisable != null)
            {
                // We found it! Now, make it disappear completely.
                theButtonToDisable.Visibility = Visibility.Collapsed;
                Debug.WriteLine("CornerCloseButton found and hidden successfully. ✅");
            }
            else
            {
                Debug.WriteLine("Could not find the CornerCloseButton control.");
            }
        }

        // A helper function to search the UI for a specific type of element.
        public static T FindChildOfType<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T)
                {
                    return (T)child;
                }
                else
                {
                    T childOfChild = FindChildOfType<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
    }
}
