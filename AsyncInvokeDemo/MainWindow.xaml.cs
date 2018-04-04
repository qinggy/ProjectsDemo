using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
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

namespace AsyncInvokeDemo
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            int contentLength = await AccessTheWebAsync();

            Loading.Text = String.Format("\r\nLength of the downloaded string: {0}.\r\n", contentLength);
        }

        async Task<int> AccessTheWebAsync()
        {
            HttpClient client = new HttpClient();
            Task<string> getStringTask = client.GetStringAsync("http://msdn.microsoft.com");
            DoIndependentWork();

            string returnContent = await getStringTask;
            return returnContent.Length;
        }

        void DoIndependentWork()
        {
            Loading.Text = "Loading ......";
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            AsyncDemo demo = new AsyncDemo();
            AsyncInvokeDemo.AsyncDemo.AsyncMethodCaller caller = new AsyncDemo.AsyncMethodCaller(demo.CallUrl);
            IAsyncResult result = caller.BeginInvoke(Callback, null);
            DoIndependentWork();
        }

        private void Callback(IAsyncResult ar)
        {
            AsyncResult result = ar as AsyncResult;
            AsyncInvokeDemo.AsyncDemo.AsyncMethodCaller caller = (AsyncInvokeDemo.AsyncDemo.AsyncMethodCaller)result.AsyncDelegate;
            string formatString = ar.AsyncState as string;

            this.Dispatcher.Invoke(() =>
            {
                Loading.Text = caller.EndInvoke(ar);
            });
        }
    }
}
