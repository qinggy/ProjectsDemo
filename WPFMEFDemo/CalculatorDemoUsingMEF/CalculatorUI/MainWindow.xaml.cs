using CompositionHelper;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculatorUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        CalciCompositionHelper objCompHelper;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            DoCalculator("Add");
        }

        private void DoCalculator(string operationType)
        {
            objCompHelper = new CalciCompositionHelper();

            //Assembles the calculator components that will participate in composition
            objCompHelper.AssembleCalculatorComponents();

            //Gets the result
            var result = objCompHelper.GetResult(Convert.ToInt32(txtFirstNumber.Text), Convert.ToInt32(txtSecondNumber.Text), operationType);

            //Display the result
            txtResult.Text = result.ToString();
        }

        private void Sub_Click(object sender, RoutedEventArgs e)
        {
            DoCalculator("Sub");
        }

        private void Mul_Click(object sender, RoutedEventArgs e)
        {
            DoCalculator("Mul");
        }

        private void Div_Click(object sender, RoutedEventArgs e)
        {
            DoCalculator("Div");
        }
    }
}
