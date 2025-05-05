using System;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Enumeration;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Controls.Primitives;
using System.Diagnostics;




namespace OperationForFolder
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private short _numberForToggleButton;

        public string caption = "Info about developer";
        public MessageBoxButton button = MessageBoxButton.OK;
        public MessageBoxImage icon = MessageBoxImage.Information;

        // для корректного закрытия программы
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            App.Current.Shutdown();
        }        

        public MainWindow()
        {
            InitializeComponent();
            LoadDirectories();
            ToggleButtonForBackground();
        }

        public void ToggleButtonForBackground()
        {
            LinearGradientBrush myLinearGradientBrush = new LinearGradientBrush();

            myLinearGradientBrush.StartPoint = new Point(0, 0);
            myLinearGradientBrush.EndPoint = new Point(1, 1);
            myLinearGradientBrush.GradientStops.Add(new GradientStop(Colors.White, 0.25));
            myLinearGradientBrush.GradientStops.Add(new GradientStop(Colors.Black, 1));

            btnS.Background = myLinearGradientBrush;
            btnT.Background = new ImageBrush(new BitmapImage(new Uri("D:\\SystemProgramIA\\OperationForFolder\\C.png")));

            _numberForToggleButton = 0;
        }

        public class DummyTreeViewItem : TreeViewItem
        {
            public DummyTreeViewItem()
                : base()
            {
                base.Header = "Dummy";
                base.Tag = "Dummy";
            }
        }

        public bool ArmorForAllButton()
        {
            if (textBlock1.Text == "" && _numberForToggleButton == 0)
            {
                MessageBox.Show("Необходимо выбран нужный узел в диске C", "Info about doing", button, icon);

                return false;
            }
            else if (textBlock2.Text == "" && _numberForToggleButton == 1)
            {
                MessageBox.Show("Необходимо выбран нужный узел в диске D", "Info about doing", button, icon);

                return false;
            }
            else if (textBlock1.Text == "" && textBlock2.Text == "")
            {
                MessageBox.Show("Необходимо выбран нужный узел в диске", "Info about doing", button, icon);

                return false;
            }
            else
            {
                return true;
            }
        }

        // основной вызов
        public void LoadDirectories()
        {
            var drives = DriveInfo.GetDrives();

            treeView.Items.Add(GetItem(drives[0]));
            treeView1.Items.Add(GetItem(drives[1]));
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            // Configure the message box to be displayed
            string messageBoxText = "Разработчик Лаптев Илья, 331 группа.\nПрограмма позволяет создавать, открывать, удалять, переименовывать, переносить и копировать папки и файлы, из разных каталогов.";

            MessageBox.Show(messageBoxText, caption, button, icon);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //    
            //закрытие программы
            //
            this.Close();
        }

        // кнопка 1 кароче сделать функцию bool с различной защитой гг
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            FileFoldersCreate fileFoldersCreate = new FileFoldersCreate();

            try
            {
                if (ArmorForAllButton() == true)
                {
                    fileFoldersCreate.Owner = this;

                    if (_numberForToggleButton == 0)
                    {
                        fileFoldersCreate.OwnerTextBlock.Text = textBlock1.Text;
                    }
                    else
                    {
                        fileFoldersCreate.OwnerTextBlock.Text = textBlock2.Text;
                    }

                    fileFoldersCreate.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        // путь дерева в textblock
        private void TreeView1_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewItem? node = e.NewValue as TreeViewItem;

            if (node != null)
                textBlock1.Text = CreatePath(node).ToString().Remove(2, 1);
        }

        private void TreeView2_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var node = e.NewValue as TreeViewItem;


            if (node != null)
                textBlock2.Text = CreatePath(node).ToString().Remove(2, 1);


        }

        // класс для реализации пути дерева для TextBlock
        StringBuilder CreatePath(TreeViewItem item)
        {
            StringBuilder path = new StringBuilder(item.Header.ToString());

            var parent = LogicalTreeHelper.GetParent(item) as TreeViewItem;


            if (parent != null)
                path.Insert(0, CreatePath(parent).ToString() + "\\");

            return path;
        }

       

        //получение дисков
        private TreeViewItem GetItem(DriveInfo drive)
        {
            var item = new TreeViewItem
            {
                Header = drive.Name,
                DataContext = drive,
                Tag = drive
            };

            AddDummy(item);

            item.Expanded += new RoutedEventHandler(item_Expanded);
            return item;
        }

        // получение каталогов
        private TreeViewItem GetItem(DirectoryInfo directory)
        {
            var item = new TreeViewItem
            {
                Header = directory.Name,
                DataContext = directory,
                Tag = directory
            };

            AddDummy(item);

            item.Expanded += new RoutedEventHandler(item_Expanded);
            return item;
        }

        // получение файлов
        private TreeViewItem GetItem(FileInfo file)
        {
            var item = new TreeViewItem
            {
                Header = file.Name,
                DataContext = file,
                Tag = file
            };

            return item;
        }

        // реализация открытия treeView
        private void AddDummy(TreeViewItem item)
        {
            item.Items.Add(new DummyTreeViewItem());
        }

        private bool HasDummy(TreeViewItem item)
        {
            return item.HasItems && (item.Items.OfType<TreeViewItem>().ToList().FindAll(tvi => tvi is DummyTreeViewItem).Count > 0);
        }

        private void RemoveDummy(TreeViewItem item)
        {
            var dummies = item.Items.OfType<TreeViewItem>().ToList().FindAll(tvi => tvi is DummyTreeViewItem);

            foreach (var dummy in dummies)
            {
                item.Items.Remove(dummy);
            }
        }

        private void ExploreDirectories(TreeViewItem item)
        {
            var directoryInfo = (DirectoryInfo)null;

            if (item.Tag is DriveInfo)
            {
                directoryInfo = ((DriveInfo)item.Tag).RootDirectory;
            }
            else if (item.Tag is DirectoryInfo)
            {
                directoryInfo = (DirectoryInfo)item.Tag;
            }
            else if (item.Tag is FileInfo)
            {
                directoryInfo = ((FileInfo)item.Tag).Directory;
            }

            if (object.ReferenceEquals(directoryInfo, null)) return;

            foreach (var directory in directoryInfo.GetDirectories())
            {
                var isHidden = (directory.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
                var isSystem = (directory.Attributes & FileAttributes.System) == FileAttributes.System;

                if (!isHidden && !isSystem)
                {
                    item.Items.Add(GetItem(directory));
                }
            }
        }

        private void ExploreFiles(TreeViewItem item)
        {
            var directoryInfo = (DirectoryInfo)null;

            if (item.Tag is DriveInfo)
            {
                directoryInfo = ((DriveInfo)item.Tag).RootDirectory;
            }
            else if (item.Tag is DirectoryInfo)
            {
                directoryInfo = (DirectoryInfo)item.Tag;
            }
            else if (item.Tag is FileInfo)
            {
                directoryInfo = ((FileInfo)item.Tag).Directory;
            }

            if (object.ReferenceEquals(directoryInfo, null)) return;

            foreach (var file in directoryInfo.GetFiles())
            {
                var isHidden = (file.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
                var isSystem = (file.Attributes & FileAttributes.System) == FileAttributes.System;

                if (!isHidden && !isSystem)
                {
                    item.Items.Add(GetItem(file));
                }
            }


        }
        private void item_Expanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;

            if (this.HasDummy(item))
            {
                this.Cursor = Cursors.Wait;
                this.RemoveDummy(item);
                this.ExploreDirectories(item);
                this.ExploreFiles(item);
                this.Cursor = Cursors.Arrow;
            }
        }

        // toggleButton крутая кнопка
        private void btnT_Checked(object sender, RoutedEventArgs e)
        {
            if (btnS != null)
            {
                DockPanel.SetDock(btnS, Dock.Right);

                LinearGradientBrush myLinearGradientBrush = new LinearGradientBrush();

                myLinearGradientBrush.StartPoint = new Point(0, 0);
                myLinearGradientBrush.EndPoint = new Point(1, 1);
                myLinearGradientBrush.GradientStops.Add(new GradientStop(Colors.Black, 0.25));
                myLinearGradientBrush.GradientStops.Add(new GradientStop(Colors.White, 1));

                btnS.Background = myLinearGradientBrush;

                btnT.Background = new ImageBrush(new BitmapImage(new Uri("D:\\SystemProgramIA\\OperationForFolder\\D.png")));

                _numberForToggleButton = 1;
            }
        }

        private void btnT_Unchecked(object sender, RoutedEventArgs e)
        {
            if (btnS != null)
            {
                DockPanel.SetDock(btnS, Dock.Left);

                ToggleButtonForBackground();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            treeView1.Items.Clear();
            treeView.Items.Clear();

            LoadDirectories();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (ArmorForAllButton() == true)
            {
                try
                {
                    if (_numberForToggleButton == 0)
                    {
                        var psi = new ProcessStartInfo(@textBlock1.Text)
                        {
                            UseShellExecute = true
                        };
                        Process.Start(psi);
                    }
                    else
                    {

                        var psi = new ProcessStartInfo(@textBlock2.Text)
                        {
                            UseShellExecute = true
                        };
                        Process.Start(psi);
                    }
                }
                catch
                {
                    MessageBox.Show("Данный файл невозможно открыть", "Info about error", button, MessageBoxImage.Error);
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (ArmorForAllButton() == true)
            {
                try
                {
                    if (_numberForToggleButton == 0)
                    {
                        FileInfo fileInf = new FileInfo(textBlock1.Text);

                        DirectoryInfo dirInfo = new DirectoryInfo(textBlock1.Text);

                        if (fileInf.Exists)
                        {
                            fileInf.Delete();
                        }
                        else if (dirInfo.Exists)
                        {
                            dirInfo.Delete(true);
                        }

                        MessageBox.Show("Файл или каталог был успешно удален", "Info about doing", button, icon);
                    }
                    else
                    {
                        FileInfo fileInf = new FileInfo(textBlock2.Text);

                        DirectoryInfo dirInfo = new DirectoryInfo(textBlock2.Text);

                        if (fileInf.Exists)
                        {
                            fileInf.Delete();
                        }
                        else if (dirInfo.Exists)
                        {
                            dirInfo.Delete(true);
                        }

                        MessageBox.Show("Файл или каталог был успешно удален", "Info about doing", button, icon);
                    }
                }
                catch
                {
                    MessageBox.Show("Данный файл или каталог нельзя удалить", "Info about doing", button, icon);
                }
            }



        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            RenameFileFilder renameFileFilder = new RenameFileFilder();

            if (ArmorForAllButton() == true)
            {
                renameFileFilder.Owner = this;

                if (_numberForToggleButton == 0)
                {
                    renameFileFilder.OwnerTextBlock1.Text = textBlock1.Text;
                }
                else
                {
                    renameFileFilder.OwnerTextBlock1.Text = textBlock2.Text;
                }

                renameFileFilder.ShowDialog();
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            MoveFileFolder moveFileFolders = new MoveFileFolder();

            if (ArmorForAllButton() == true)
            {
                moveFileFolders.Owner = this;

                if (_numberForToggleButton == 0)
                {
                    moveFileFolders.OwnerTextBlock2.Text = textBlock1.Text;
                }
                else
                {
                    moveFileFolders.OwnerTextBlock2.Text = textBlock2.Text;
                }

                moveFileFolders.ShowDialog();
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            CopyFileFolder copyFileFolders = new CopyFileFolder();

            if (ArmorForAllButton() == true)
            {
                copyFileFolders.Owner = this;

                if (_numberForToggleButton == 0)
                {
                    copyFileFolders.OwnerTextBlock3.Text = textBlock1.Text;
                }
                else
                {
                    copyFileFolders.OwnerTextBlock3.Text = textBlock2.Text;
                }

                copyFileFolders.ShowDialog();
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            // Configure the message box to be displayed
            string messageBoxText = "В программе вы можете заимодействовать с диском C и диском D. Ваша задача выбрать какой нибудь узел в главных окнах, затем нажать на соответствующие кнопки для завершения действия.";

            MessageBox.Show(messageBoxText, "Instruction about programm", button, icon);
        }
    }

    //class Delete<T>
    //{
    //    public T  ForDelete { get; set; }

    //    public Delete(T forDelete)
    //    {
    //        ForDelete = forDelete;
    //    }
    //}

    
}