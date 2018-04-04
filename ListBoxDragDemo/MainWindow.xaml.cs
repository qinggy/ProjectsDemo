using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ListBoxDragDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        ListBox dragSource = null;
        ObservableCollection<string> timeZoneList = new ObservableCollection<string>();
        public MainWindow()
        {
            InitializeComponent();

            foreach (TimeZoneInfo timeZoneInfo in TimeZoneInfo.GetSystemTimeZones())
            {
                timeZoneList.Add(timeZoneInfo.ToString());
            }

            lbOne.DataContext = timeZoneList;
        }

        private void lbOne_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListBox listSource = (ListBox)sender;
            dragSource = listSource;
            object data = GetDataFromListBox(dragSource, e.GetPosition(listSource));

            if (data != null)
            {
                DragDrop.DoDragDrop(listSource, data, DragDropEffects.Move);
            }
        }

        private static object GetDataFromListBox(ListBox source, Point point)
        {
            UIElement element = source.InputHitTest(point) as UIElement;
            if (element != null)
            {
                object data = DependencyProperty.UnsetValue;
                while (data == DependencyProperty.UnsetValue)
                {
                    data = source.ItemContainerGenerator.ItemFromContainer(element);
                    if (data == DependencyProperty.UnsetValue)
                    {
                        element = VisualTreeHelper.GetParent(element) as UIElement;
                    }
                    if (element == source)
                    {
                        return null;
                    }
                }
                if (data != DependencyProperty.UnsetValue)
                {
                    return data;
                }
            }
            return null;
        }
        

        private void lbTwo_Drop(object sender, DragEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            object data = e.Data.GetData(typeof(string));
            ((IList)dragSource.ItemsSource).Remove(data);
            parent.Items.Add(data);
        }
    }
}
