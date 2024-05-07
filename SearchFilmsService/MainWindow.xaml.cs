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

namespace SearchFilmsService
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MovieService _movieService;

        public MainWindow()
        {
            InitializeComponent();
            _movieService = new MovieService("5b2ded6c");
        }

        private async void GetMovieInfo_Click(object sender, RoutedEventArgs e)
        {
            string movieTitle = movieTextBox.Text;
            string movieInfo = await _movieService.GetMovieInfo(movieTitle);
            movieInfoTextBlock.Text = movieInfo;
        }
    }
}
