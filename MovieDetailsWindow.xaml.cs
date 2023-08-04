using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
using System.Windows.Shapes;
using static MovieSearchApp.MainWindow;

namespace MovieSearchApp
{
    /// <summary>
    /// Interaction logic for MovieDetailsWindow.xaml
    /// </summary>
    public partial class MovieDetailsWindow : Window
    {
        private MovieDetails movieDetails;
        private const string apiKey = "64d8d178";
        private const string baseApiUrl = "https://www.omdbapi.com/";

        public MovieDetailsWindow(Movie movie)
        {
            InitializeComponent();
            GetMovieDetails(movie.ImdbID);
        }

        private async void GetMovieDetails(string imdbId)
        {
            string apiUrl = $"{baseApiUrl}?apikey={apiKey}&i={imdbId}";
            using (var httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        movieDetails = JsonConvert.DeserializeObject<MovieDetails>(jsonResponse);

                        textMovieTitle.Text = movieDetails.Title;
                        textYear.Text = movieDetails.Year;
                        textRated.Text = movieDetails.Rated;
                        textReleased.Text = movieDetails.Released;
                        textRuntime.Text = movieDetails.Runtime;
                        textGenre.Text = movieDetails.Genre;
                        textDirector.Text = movieDetails.Director;
                        textWriter.Text = movieDetails.Writer;
                        textActors.Text = movieDetails.Actors;
                        textPlot.Text = movieDetails.Plot;
                        textLanguage.Text = movieDetails.Language;
                        imgPoster.Source = new BitmapImage(new Uri(movieDetails.Poster));

                    }
                    else
                    {
                        MessageBox.Show($"HTTP Error: {response.StatusCode}");
                    }
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
