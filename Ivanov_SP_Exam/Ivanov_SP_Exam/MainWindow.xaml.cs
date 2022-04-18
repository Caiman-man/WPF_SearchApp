using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SearchLibrary;

namespace Ivanov_SP_Exam
{
    public partial class MainWindow : Window
    {
        //обьект класса из сборки 
        SearchClass search = null;

        //объект для управления плагинами
        PlugInManager plugInManager;


        public MainWindow()
        {
            InitializeComponent();
            plugInManager = new PlugInManager(this);
            pluginCB.ItemsSource = plugInManager.GetPlugInsNames();
            progressBar.Visibility = Visibility.Hidden;
        }

        //start
        private void searchB_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                search = new SearchClass(pathTB.Text, maskTB.Text);
                DataContext = search;
                progressBar.Visibility = Visibility.Visible;
            }
            catch { }
        }

        //pause
        private void pauseB_Click(object sender, RoutedEventArgs e)
        {
            Thread tmp = search.ThreadProp;
            if (tmp.IsAlive)
                tmp.Suspend();

            stopB.IsEnabled = false;
            searchB.IsEnabled = false;
            resumeB.IsEnabled = true;
            progressBar.Visibility = Visibility.Hidden;
        }

        //resume
        private void resumeB_Click(object sender, RoutedEventArgs e)
        {
            Thread tmp = search.ThreadProp;
            if (tmp.IsAlive)
                tmp.Resume();

            stopB.IsEnabled = true;
            searchB.IsEnabled = false;
            progressBar.Visibility = Visibility.Visible;
        }

        //stop
        private void stopB_Click(object sender, RoutedEventArgs e)
        {
            Thread tmp = search.ThreadProp;
            if (tmp.IsAlive)
            {
                tmp?.Abort();
                tmp = null;
            }
            searchB.IsEnabled = true;
            resumeB.IsEnabled = true;
            progressBar.Visibility = Visibility.Hidden;
        }

        //exit
        private void exitB_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //очистить path
        private void clearB1_Click(object sender, RoutedEventArgs e)
        {
            pathTB.Text = "";
        }

        //очистить mask
        private void clearB2_Click(object sender, RoutedEventArgs e)
        {
            maskTB.Text = "";
        }

        //активировать тему
        private void pluginCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            plugInManager.ActivatePlugIn(pluginCB.SelectedItem.ToString());
        }

        //double click
        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FileData selectedRow = dataGrid.SelectedItem as FileData;
            if (selectedRow.Name.Contains(".exe") || selectedRow.Name.Contains(".mp3") || selectedRow.Name.Contains(".txt"))
                Process.Start(selectedRow.Path);
        }
    }
}
