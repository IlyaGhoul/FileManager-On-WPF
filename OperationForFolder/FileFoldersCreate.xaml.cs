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
using System.Xml.Linq;

namespace OperationForFolder
{
    /// <summary>
    /// Логика взаимодействия для FileFoldersCreate.xaml
    /// </summary>
    public partial class FileFoldersCreate : Window
    {
        public FileFoldersCreate()
        {
            InitializeComponent();
        }

        public void CreateFileFolders()
        {
            MainWindow mainWindow = new MainWindow();
            
            string path = @OwnerTextBlock.Text + "\\" +  OwnertextBox1.Text;
            

            FileInfo fileInf = new FileInfo(path);
            if (OwnertextBox1.Text == " ")
            {
                MessageBox.Show("Необходимо указать название файла", "Information about Error", mainWindow.button, mainWindow.icon);
            }
            else
            {
                if (fileInf.Exists)
                {
                    MessageBox.Show("Такой файл уже существует!", "Information about activities", mainWindow.button, mainWindow.icon);
                }
                else
                {
                    FileStream fileStream = fileInf.Create();

                    fileStream.Close();

                    MessageBox.Show("Файл успешно создан", "Information about activities", mainWindow.button, mainWindow.icon);
                }
            }                          
        }

        public void CreateDirecctory()
        {
            MainWindow mainWindow = new MainWindow();

            string path = @OwnerTextBlock.Text + "\\" + OwnertextBox1.Text;

            FileInfo fileInf = new FileInfo(path);


            if (OwnertextBox1.Text == " ")
            {
                MessageBox.Show("Необходимо указать название каталога", "Information about Error", mainWindow.button, mainWindow.icon);
            }
            else
            {
                if (fileInf.Exists)
                {
                    MessageBox.Show("Такой каталог уже существует!", "Information about activities", mainWindow.button, mainWindow.icon);
                }

                else
                {
                    try
                    {
                        Directory.CreateDirectory(path);

                        MessageBox.Show("Каталог успешно создан", "Information about activities", mainWindow.button, mainWindow.icon);
                    }
                    catch (System.IO.IOException)
                    {
                        MessageBox.Show("Нельзя сохранить файл или каталог в файл!", "Information about activities", mainWindow.button, MessageBoxImage.Error);
                    }
                    
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateFileFolders();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreateDirecctory();
        }
    }
}
