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
    /// Логика взаимодействия для MoveFileFolder.xaml
    /// </summary>
    public partial class MoveFileFolder : Window
    {
        public MoveFileFolder()
        {
            InitializeComponent();
        }

        public void MoveFileFolders()
        {
            MainWindow mainWindow = new MainWindow();

            string oldPath = @OwnerTextBlock2.Text;
            string newPath = @OwnertextBox2.Text;
              

            DirectoryInfo dirInfo = new(oldPath);
            FileInfo fileInf = new FileInfo(oldPath);

            if (OwnertextBox2.Text == " ")
            {
                MessageBox.Show("Необходимо указать путь куда переместить указанную папку или файл", "Information about Error", mainWindow.button, mainWindow.icon);
            }
            else
            {
                try
                {
                    if (dirInfo.Exists)
                    {

                        dirInfo.MoveTo(newPath);

                        MessageBox.Show("Каталог был успешно перемещен", "Info about doing", mainWindow.button, mainWindow.icon);
                    }
                    else if (fileInf.Exists)
                    {

                        fileInf.MoveTo(newPath);

                        MessageBox.Show("Файл был успешно перемещен", "Info about doing", mainWindow.button, mainWindow.icon);
                    }
                    else
                    {
                        MessageBox.Show("Проверьте правильность введенного вами пути!", "Info about doing", mainWindow.button, mainWindow.icon);
                    }
                }
                catch
                {
                    if(oldPath == newPath)
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

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            MoveFileFolders();
        }
    }
}
