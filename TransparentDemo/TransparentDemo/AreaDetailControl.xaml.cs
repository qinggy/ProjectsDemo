using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace IS.WPF.Controls
{
    /// <summary>
    /// AreaDetailControl.xaml 的交互逻辑
    /// </summary>
    public partial class AreaDetailControl : UserControl
    {
        bool IsExpand = false;

        public AreaDetailControl()
        {
            InitializeComponent();

            this.Loaded += AreaDetailControl_Loaded;
        }

        void AreaDetailControl_Loaded(object sender, RoutedEventArgs e)
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.AutoReset = true;
            timer.Elapsed += (s, ee) =>
            {
                if (IsExpand)
                {
                    if (ProcessEvent != null)
                    {
                        ProcessEvent.Invoke();
                    }
                }

            };
            timer.Interval = Interval * 1000;
            timer.Enabled = true;
        }

        public event Action ProcessEvent;

        public int Interval
        {
            get { return (int)GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Interval.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.Register("Interval", typeof(int), typeof(AreaDetailControl), new PropertyMetadata(4, new PropertyChangedCallback(IntervalChanged)));

        private static void IntervalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) { }

        public string PresentContent
        {
            get { return (string)GetValue(PresentContentProperty); }
            set { SetValue(PresentContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentContentProperty =
            DependencyProperty.Register("PresentContent", typeof(string), typeof(AreaDetailControl), new PropertyMetadata(string.Empty));

        public string PresentTitle
        {
            get { return (string)GetValue(PresentTitleProperty); }
            set { SetValue(PresentTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PresentTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PresentTitleProperty =
            DependencyProperty.Register("PresentTitle", typeof(string), typeof(AreaDetailControl), new PropertyMetadata(string.Empty));

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            Image img = sender as Image;
            if (img.Tag.ToString() == "expand")
            {
                IsExpand = true;
                img.Tag = "collspe";
                img.Source = new BitmapImage(new Uri("collspe.png", UriKind.Relative));

                DoubleAnimation exAnimation = new DoubleAnimation();
                exAnimation.Duration = TimeSpan.FromSeconds(0.4);
                exAnimation.To = this.Sp.ActualHeight + 10;
                ContentPresenter.BeginAnimation(Border.HeightProperty, exAnimation);

                ThicknessAnimation marAnimation = new ThicknessAnimation();
                marAnimation.Duration = TimeSpan.FromSeconds(0.4);
                marAnimation.To = new Thickness(0, -(this.Sp.ActualHeight + 40), 0, 0);
                ActualContent.BeginAnimation(Border.MarginProperty, marAnimation);

                DoubleAnimation heAnimation = new DoubleAnimation();
                heAnimation.Duration = TimeSpan.FromSeconds(0.4);
                heAnimation.To = this.Sp.ActualHeight;
                Sp.BeginAnimation(StackPanel.HeightProperty, exAnimation);

                //ThicknessAnimation corAnimation = new ThicknessAnimation();
                //corAnimation.Duration = TimeSpan.FromSeconds(0.4);
                //corAnimation.To = new Thickness(8, 8, 0, 0);
                //ContentTitle.BeginAnimation(Border.CornerRadiusProperty, corAnimation);
                //ContentTitle.CornerRadius = new CornerRadius(6, 6, 0, 0);
            }
            else if (img.Tag.ToString() == "collspe")
            {
                IsExpand = false;
                img.Tag = "expand";
                img.Source = new BitmapImage(new Uri("expand.png", UriKind.Relative));

                DoubleAnimation exAnimation = new DoubleAnimation();
                exAnimation.Duration = TimeSpan.FromSeconds(0.4);
                exAnimation.To = 0;
                ContentPresenter.BeginAnimation(Border.HeightProperty, exAnimation);

                ThicknessAnimation marAnimation = new ThicknessAnimation();
                marAnimation.To = new Thickness(0, -30, 0, 0);
                marAnimation.Duration = TimeSpan.FromSeconds(0.4);
                ActualContent.BeginAnimation(Border.MarginProperty, marAnimation);

                DoubleAnimation heAnimation = new DoubleAnimation();
                heAnimation.Duration = TimeSpan.FromSeconds(0.4);
                heAnimation.To = 0;
                Sp.BeginAnimation(StackPanel.HeightProperty, exAnimation);
                //this.ContentTitle.CornerRadius = new CornerRadius(6);
            }
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            Image img = sender as Image;
            if (img.Tag.ToString() == "expand")
            {
                img.ToolTip = "展开";
            }
            else if (img.Tag.ToString() == "collspe")
            {
                img.ToolTip = "收缩";
            }
        }
    }
}
