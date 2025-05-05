using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OperationForFolder
{
    /// <summary>
    /// Логика взаимодействия для CopyFileFolder.xaml
    /// </summary>
    public partial class CopyFileFolder : Window
    {
        public CopyFileFolder()
        {
            InitializeComponent();
        }

        public void CopyFileFolders()
        {
            MainWindow mainWindow = new MainWindow();

            string oldPath = @OwnerTextBlock3.Text;
            string newPath = @OwnertextBox3.Text;


            DirectoryInfo dirInfo = new(oldPath);
            FileInfo fileInf = new FileInfo(oldPath);

            if (OwnertextBox3.Text == " ")
            {
                MessageBox.Show("Необходимо указать путь куда переместить указанную папку или файл", "Information about Error", mainWindow.button, mainWindow.icon);
            }
            else
            {
                try
                {
                    if (dirInfo.Exists)
                    {
                        string source_dir = oldPath;
                        string destination_dir = newPath;

                        if (Directory.Exists(newPath) == false)
                        {
                            Directory.CreateDirectory(newPath);                  
                        }

                        foreach (string dir in System.IO.Directory.GetDirectories(source_dir, "*", System.IO.SearchOption.AllDirectories))
                        {
                            System.IO.Directory.CreateDirectory(System.IO.Path.Combine(destination_dir, dir.Substring(source_dir.Length + 1)));
                            // Example:
                            //     > C:\sources (and not C:\E:\sources)
                        }

                        foreach (string file_name in System.IO.Directory.GetFiles(source_dir, "*", System.IO.SearchOption.AllDirectories))
                        {
                            System.IO.File.Copy(file_name, System.IO.Path.Combine(destination_dir, file_name.Substring(source_dir.Length + 1)));
                        }

                        MessageBox.Show("Каталог был успешно перемещен", "Info about doing", mainWindow.button, mainWindow.icon);

                    }
                    else if (fileInf.Exists)
                    {

                        fileInf.CopyTo(newPath, true);

                        MessageBox.Show("Файл был успешно перемещен", "Info about doing", mainWindow.button, mainWindow.icon);
                    }
                    else
                    {
                        MessageBox.Show("Проверьте правильность введенного вами пути!", "Info about doing", mainWindow.button, mainWindow.icon);
                    }
                }
                catch
                {
                    if (oldPath == newPath)
                    {
                        MessageBox.Show("Новый путь должен отличаться от старого!", "Info about error", mainWindow.button, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show("В конце пути укажите или тот же файл или дайте ему новое название!", "Info about error", mainWindow.button, MessageBoxImage.Error);
                    }
                }


            }
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            CopyFileFolders();
        }
    }
}
