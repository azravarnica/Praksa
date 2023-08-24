using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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
        private const string baseApiUrl = "http://localhost:5148/api/Movie";
        private string messageError = "";
        private List<Movie> movies;
        private int currentPage = 1;
        private int pageSize = 10;

        public MovieDetailsWindow(Movie movie)
        {
            InitializeComponent();
            GetMovieDetails(movie.ImdbID);
        }

        private async void GetMovieDetails(int imdbId)
        {
            string apiUrl = $"{baseApiUrl}/{imdbId}";
            using (var httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        movieDetails = JsonConvert.DeserializeObject<MovieDetails>(jsonResponse);

                        movieDetails.Title = movieDetails.Title.ToUpper();
                        DataContext = movieDetails;

                        try {
                            imgPoster.Source = new BitmapImage(new Uri(movieDetails.Poster));
                        } catch (Exception) {
                            
                            imgPoster.Source = new BitmapImage(new Uri("cannotBeLoaded.jpg", UriKind.Relative));

                        }
                      
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

        private void btnUpdateMovie_Click(object sender, RoutedEventArgs e)
        {
            UpdateMovie updateMovie = new UpdateMovie(movieDetails);
            updateMovie.ShowDialog();
            GetMovieDetails(movieDetails.ImdbID);
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            bool isSuccess = await DeleteMovie(movieDetails.ImdbID);
            if (isSuccess)
            {
                MessageBox.Show($"Successfully deleted movie");
                this.Close();
            }
            else
            {
                MessageBox.Show($"HTTP Error : {messageError}");
            }

        }

        private async Task<bool> DeleteMovie(int movieDetailsId)
        {
            string apiUrl = $"{baseApiUrl}/{movieDetailsId}";

            using (var hattpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await hattpClient.DeleteAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        return true;

                    }
                    else
                    {
                        messageError = response.StatusCode.ToString();
                        return false;
                    }
                }
                catch (HttpRequestException ex)
                {
                    messageError = ex.Message;
                    return false;
                }

            }
        }

    }
}
