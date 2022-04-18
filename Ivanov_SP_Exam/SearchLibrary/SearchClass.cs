using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Windows;

namespace SearchLibrary
{
    /// <summary>
    /// класс производит рекурсивный поиск файлов внутри отдельного потока
    /// </summary>
    public class SearchClass
    {
        //поток
        Thread t = null;

        //переменные для пути и маски
        string path = "";
        string mask = "";

        //коллекция для передачи данных в UI
        public ObservableCollection<FileData> files = new ObservableCollection<FileData>();

        //свойства
        public Thread ThreadProp 
        { 
            get { return t; }
            set { t = value; }
        }

        public ObservableCollection<FileData> Files
        {
            get { return files; }
            set { files = value; }
        }


        //конструктор
        public SearchClass(string _path, string _mask)
        {
            this.path = _path;
            this.mask = _mask;
            ParameterizedThreadStart p = new ParameterizedThreadStart(thread);
            t = new Thread(p);
            t.IsBackground = true;
            t.Start();
        }

        //потоковая функция
        void thread(object obj)
        {
            ShowAllFiles(path, mask);
        }

        //функция поиска
        public void ShowAllFiles(string path, string mask)
        {
            DirectoryInfo dinfo = new DirectoryInfo(path);

            if (dinfo.Exists)
            {
                try
                {
                    FileInfo[] files = dinfo.GetFiles(mask);
                    foreach (FileInfo current in files)
                    {
                        //выводим данные в UI
                        FileData result = new FileData(current.Name, current.Extension, current.FullName, current.Length, current.CreationTime);
                        Application.Current.Dispatcher.Invoke(() => Files.Add(result));
                        Thread.Sleep(10);
                    }

                    DirectoryInfo[] dirs = dinfo.GetDirectories();
                    foreach (DirectoryInfo current in dirs)
                    {
                        ShowAllFiles(path + @"\" + current.Name, mask);
                    }
                }
                catch { }
            }
            else
                MessageBox.Show("Path is not exists");
        }
    }



    /// <summary>
    /// класс хранящий в себе данные о файле
    /// </summary>
    public class FileData
    {
        public string Name { get; set; }
        public string Extension { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
        public DateTime CreationTime { get; set; }

        public FileData(string name, string ext, string path, long size, DateTime date)
        {
            this.Name = name;
            this.Extension = ext;
            this.Path = path;
            this.Size = size;
            this.CreationTime = date;
        }
    }
}
