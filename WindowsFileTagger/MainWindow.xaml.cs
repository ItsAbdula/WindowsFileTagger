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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WindowsFileTagger
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private TreeViewItem dummyNode = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MakeDirectories();
        }

        private void MakeDirectories()
        {
            foreach (var driver in Directory.GetLogicalDrives())
            {
                var item = new TreeViewItem();
                item.Header = driver;
                item.Tag = driver;
                item.Expanded += new RoutedEventHandler(folderExpanded);

                // Expanded가능하다는 아이콘 보여주기 위해, 더미 아이템 추가.
                // TODO: 폴더에 파일이 하나라도 있을때, 아이콘 보여주기.
                item.Items.Add(dummyNode);

                FileExplorer.Items.Add(item);
            }
        }

        private void folderExpanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;

            if (item.Items.Count == 1 && item.Items[0] == null)
            {
                item.Items.Clear();

                foreach (string folder in Directory.GetDirectories(item.Tag.ToString()))
                {
                    var subitem = new TreeViewItem();
                    subitem.Header = folder.Substring(folder.LastIndexOf("\\") + 1);
                    subitem.Tag = folder;
                    subitem.Items.Add(dummyNode);

                    subitem.Expanded += new RoutedEventHandler(folderExpanded);

                    item.Items.Add(subitem);
                }
            }
        }
    }
}