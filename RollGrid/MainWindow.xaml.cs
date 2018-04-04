using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RollGrid
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            (this.FindName("storyboard") as Storyboard).Stop();

        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            (this.FindName("storyboard") as Storyboard).Begin();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Page1 page = new Page1();
            page.Show();
        }

        //private void CanvasContainer_Loaded(object sender, RoutedEventArgs e)
        //{
        //    DoubleAnimation animation = new DoubleAnimation();
        //    animation.From = 0;
        //    animation.To = -850;
        //    animation.Duration = TimeSpan.FromSeconds(3);
        //    animation.RepeatBehavior = RepeatBehavior.Forever;
        //    this.CanvasContainer.BeginAnimation(Canvas.RenderTransformProperty, animation);
        //}
    }
}
