using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace WeatherForecast
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly WeatherService _weatherService;

        public MainWindow()
        {
            InitializeComponent();
            _weatherService = new WeatherService("4a3f544c-5c6c-4e83-8ef5-0a56699c1621");
        }

        private async void GetWeather_Click(object sender, RoutedEventArgs e)
        {
            string city = cityTextBox.Text;
            string weather = await _weatherService.GetWeather(city);
            weatherTextBlock.Text = weather;
        }
    }
}
