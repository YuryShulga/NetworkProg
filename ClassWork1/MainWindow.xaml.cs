using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
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

namespace ClassWork1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Send_Click(object sender, RoutedEventArgs e)
        {
            //using (HttpClient client = new HttpClient())
            //{
            //    using HttpResponseMessage response =
            //         await client.GetAsync("http://www.gramota.ru/");
            //    string body = await response.Content.ReadAsStringAsync();
            //    textBoxBody.Text = body;

            //    HttpSatusCode status = response.StatusCode;
            //}

            using HttpClient client = new HttpClient();
            using var response = await client.GetAsync(TextBox_Adress.Text);
            //string content = await client.GetStringAsync(TextBox_Adress.Text);
            //TextBox_Content.Text = content.bo
            var content = await response.Content.ReadAsStringAsync();
            TextBox_Content.Text = content;
            TextBlock_AnswerCode.Text = response.StatusCode.ToString();
            
        }
    }
}
