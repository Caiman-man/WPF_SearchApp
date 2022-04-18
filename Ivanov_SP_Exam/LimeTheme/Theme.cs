using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LimeTheme
{
    public class Theme
    {
        public void ChangeWindow(Window window)
        {
            window.Background = new SolidColorBrush(Colors.LightBlue);

            if (window.Content is Grid)
            {
                Grid mainContainer = (Grid)window.Content;
                mainContainer.Background = new SolidColorBrush(Color.FromRgb(215, 255, 79));

                foreach (UIElement currentElement in mainContainer.Children)
                {
                    if (currentElement is Grid)
                    {
                        ((Grid)currentElement).Background = new SolidColorBrush(Color.FromRgb(215, 255, 79));
                    }
                    else if (currentElement is Button)
                    {
                        ((Button)currentElement).Background = new SolidColorBrush(Color.FromRgb(150, 178, 55));
                        ((Button)currentElement).BorderBrush = new SolidColorBrush(Color.FromRgb(150, 178, 55));
                    }
                    else if (currentElement is Label)
                    {
                        ((Label)currentElement).Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    }
                }
            }
        }
    }
}
