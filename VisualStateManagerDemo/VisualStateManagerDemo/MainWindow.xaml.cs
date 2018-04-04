using System.Collections.Generic;
using System.Windows;

namespace VisualStateManagerDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        List<TreeItem> TreeItems = new List<TreeItem>();
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            TreeItem item = new TreeItem
            {
                Id = 1,
                Name = "中国",
                ParentId = 0
            };
            TreeItems.Add(item);

            TreeItem item01 = new TreeItem
            {
                Id = 2,
                Name = "湖南",
                ParentId = 1
            };
            TreeItems.Add(item01);

            TreeItem item02 = new TreeItem
            {
                Id = 3,
                Name = "长沙",
                ParentId = 2
            };
            TreeItems.Add(item02);

            TreeItem item03 = new TreeItem
            {
                Id = 4,
                Name = "邵阳",
                ParentId = 2
            };
            TreeItems.Add(item03);

            TreeItem item04 = new TreeItem
            {
                Id = 5,
                Name = "洞口",
                ParentId = 4
            };
            TreeItems.Add(item04);

            TreeItem item05 = new TreeItem
            {
                Id = 6,
                Name = "广东",
                ParentId = 1
            };
            TreeItems.Add(item05);
            this.TreeNodes.DataContext = TreeItems;

        }

    }

    class TreeItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ParentId { get; set; }
    }
}
