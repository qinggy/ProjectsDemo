using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SimpleWCF.Client
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 说明：单击“计算”按钮，得到计算结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            SquareService.SquareServiceClient clientSquare = new SquareService.SquareServiceClient();
            clientSquare.GetSquareValueCompleted += new EventHandler<SquareService.GetSquareValueCompletedEventArgs>(clientSquare_GetSquareValueCompleted);

            clientSquare.GetSquareValueAsync(double.Parse(this.txtNumber.Text), int.Parse(this.txtN.Text));
        }

        /// <summary>
        /// 说明：将计算结果显示在下方的文本框中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void clientSquare_GetSquareValueCompleted(object sender, SquareService.GetSquareValueCompletedEventArgs e)
        {
            this.txtResultValue.Text = "计算结果：" + e.Result;
        }
    }
}
