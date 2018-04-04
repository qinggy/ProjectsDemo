using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverlightInMvcframework
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void b1_Click(object sender, RoutedEventArgs e)
        {
            WebClient _client = new WebClient();
            _client.OpenReadCompleted += new OpenReadCompletedEventHandler(_client_OpenReadCompleted);

            _client.OpenReadAsync(new Uri("http://localhost:38607/home/list"));
        }

        private void _client_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(List<Test>));
            
            IList<Test> res = (List<Test>)(json.ReadObject(e.Result));

            MyGrid.ItemsSource = res;
        }
    }
}
