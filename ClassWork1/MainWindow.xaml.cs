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
using Microsoft.Win32;
using System.IO;
using System.Windows.Threading;

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
            try
            {
                using var response = await client.GetAsync(TextBox_Adress.Text);
                var content = await response.Content.ReadAsStringAsync();
                await Dispatcher.InvokeAsync(() => TextBox_Content.Text = content);

                TextBlock_Result.Text = $"({(int)(response.StatusCode)}){response.StatusCode.ToString()}";

                foreach (var item in response.Headers)
                {
                    
                    StringBuilder sb = new();
                    sb.Append($"\"{item.Key}\"  -  ");
                    foreach (var value in item.Value)
                    {
                        sb.Append($"{value.ToString()}, ");
                    }
                    ListBox_Headers.Items.Add(sb.ToString());
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Неправильно введен запрос");
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("Отсутствие соединения с интернетом.");
            }

        }

        private async void Button_SaveRequestContentToFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new();
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(dialog.FileName))
                    {       
                        
                        await writer.WriteLineAsync(TextBox_Content.Text);
                    }
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Произошла ошибка при сохранении файла: " + ex.Message);
                }
            }
        }


    }
}
