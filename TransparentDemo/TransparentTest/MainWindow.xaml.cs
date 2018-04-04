using IS.WPF.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TransparentTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Point targetPoint;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainCanvas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var targetElement = e.Source as IInputElement;
            if (targetElement != null)
            {
                targetPoint = e.GetPosition(targetElement);
                targetElement.CaptureMouse();
            }
        }

        private void MainCanvas_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            var targetElement = Mouse.Captured as UIElement;
            if (e.LeftButton == MouseButtonState.Pressed && targetElement != null)
            {
                var pCanvas = e.GetPosition(MainCanvas);
                //set final position
                Canvas.SetLeft(targetElement, pCanvas.X - targetPoint.X);
                Canvas.SetTop(targetElement, pCanvas.Y - targetPoint.Y);

            }
        }

        private void MainCanvas_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
        }

        private void Process()
        {
            MessageBox.Show("南方水泥");
        }

        private void ProcessOne()
        {
            MessageBox.Show("万达广场");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("查询条件");
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            ((MediaElement)sender).Position = ((MediaElement)sender).Position.Add(TimeSpan.FromMilliseconds(1));
        }
    }
}