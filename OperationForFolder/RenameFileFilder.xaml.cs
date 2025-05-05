using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для RenameFileFilder.xaml
    /// </summary>
    public partial class RenameFileFilder : Window
    {
        public RenameFileFilder()
        {
            InitializeComponent();
        }

        public void RenameFileFolders()
        {
            MainWindow mainWindow = new MainWindow();

            string path = @OwnerTextBlock1.Text;

            string[] newPath = path.Split('\\');
            Array.Resize(ref newPath, newPath.Length - 1);
           
            newPath = newPath.Append(OwnertextBox1.Text).ToArray();

            path = string.Join("\\", newPath);

            DirectoryInfo dirInfo = new(@OwnerTextBlock1.Text);
            FileInfo fileInf = new FileInfo(@OwnerTextBlock1.Text);

            if (OwnertextBox1.Text == " ")
            {
                MessageBox.Show("Необходимо указать название нового файла", "Information about Error", mainWindow.button, mainWindow.icon);
            }
            else
            {
                try
                {
                    if (dirInfo.Exists && !Directory.Exists(path))
                    {
                        dirInfo.MoveTo(path);
                    }
                    else if (fileInf.Exists)
                    {
                        fileInf.MoveTo(path);
                    }

                    MessageBox.Show("Файл или каталог был успешно переименован", "Info about doing", mainWindow.button, mainWindow.icon);
                }
                catch
                {
                    MessageBox.Show("Файл или каталог был успешно переименован", "Info about doing", mainWindow.button, mainWindow.icon);
                }
                
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RenameFileFolders();
        }
    }
}
