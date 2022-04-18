using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DefaultTheme
{
    public class Theme
    {
        public void ChangeWindow(Window window)
        {
            window.Background = new SolidColorBrush(Colors.LightBlue);

            if (window.Content is Grid)
            {
                Grid mainContainer = (Grid)window.Content;
                mainContainer.Background = new SolidColorBrush(Color.FromRgb(12, 7, 21));

                foreach (UIElement currentElement in mainContainer.Children)
                {
                    if (currentElement is Grid)
                    {
                        ((Grid)currentElement).Background = new SolidColorBrush(Color.FromRgb(12, 7, 21));
                    }
                    else if (currentElement is Button)
                    {
                        ((Button)currentElement).Background = new SolidColorBrush(Color.FromRgb(29, 185, 84));
                        ((Button)currentElement).BorderBrush = new SolidColorBrush(Color.FromRgb(29, 185, 84));
                    }
                    else if (currentElement is Label)
                    {
                        ((Label)currentElement).Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    }
                }
            }
        }
    }
}
