using Newtonsoft.Json.Linq;
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
using System.Windows.Shapes;

namespace MovieSearchApp
{
    /// <summary>
    /// Interaction logic for MovieDetailsWindow.xaml
    /// </summary>
    public partial class MovieDetailsWindow : Window
    {
        public MovieDetailsWindow()
        {
            InitializeComponent();
        }

        public MovieDetailsWindow(JObject movieDetails)
        {
            InitializeComponent();
            DisplayMovieDetails(movieDetails);
        }

        private void DisplayMovieDetails(JObject movieDetails)
        {
            textMovieTitle.Text = movieDetails["Title"].ToString();
            textYear.Text = movieDetails["Year"].ToString();
            textRated.Text = movieDetails["Rated"].ToString();
            textReleased.Text = movieDetails["Released"].ToString();
            textRuntime.Text = movieDetails["Runtime"].ToString();
            textGenre.Text = movieDetails["Genre"].ToString();
            textDirector.Text = movieDetails["Director"].ToString();
            textWriter.Text = movieDetails["Writer"].ToString();
            textActors.Text = movieDetails["Actors"].ToString();
            textPlot.Text = movieDetails["Plot"].ToString();
            textLanguage.Text = movieDetails["Language"].ToString();
            imgPoster.Source = new BitmapImage(new Uri(movieDetails["Poster"].ToString()));

        }
    }
}
