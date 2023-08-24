using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Net.WebRequestMethods;
using static MovieSearchApp.MainWindow;

namespace MovieSearchApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string baseApiUrl = "http://localhost:5148/api/Movie";
        public MainWindow()
        {
            InitializeComponent();
            btnNextPage.Visibility = Visibility.Collapsed;
        }
        private List<Movie> movies;
        private int currentPage = 1;
        private int pageSize = 10;
        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            currentPage = 1;
            string searchText = textSearch.Text;
            if (string.IsNullOrEmpty(searchText))
            {
                MessageBox.Show("Enter a search term!");
                return;

            }
            else if (searchText.Length > 8)
            {
                lblEnter.Foreground = new SolidColorBrush(Colors.Red);
                MessageBox.Show("Term is too long!");
                return;
            }

            lblEnter.Foreground = new SolidColorBrush(Colors.Black);
            movies = await SearchMovies(searchText, currentPage, pageSize);

            if (movies.Count == 0)
            {
                MessageBox.Show("No movies found.");
            }
            else
            {
                listMovies.ItemsSource = movies;
                btnNextPage.Visibility = movies.Count > 9 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private async void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            
            string searchText = textSearch.Text;
            if (string.IsNullOrEmpty(searchText))
            {
                MessageBox.Show("Enter a search term!");
                return;

            }
            else if (searchText.Length > 8)
            {
                MessageBox.Show("Term is too long!");
                return;
            }
            currentPage++;
            var nextMovies = await SearchMovies(searchText, currentPage, pageSize);
            listMovies.ItemsSource = nextMovies;
            btnNextPage.Visibility = nextMovies.Count > 9 ? Visibility.Visible : Visibility.Collapsed;
        }

        private async Task<List<Movie>> SearchMovies(string searchTerm, int page, int pageSize)
        {
            using (var httpClient = new HttpClient())
            {
              try
              {
                    string apiUrl = $"{baseApiUrl}/search?searchTerm={searchTerm}&page={page}&pageSize={pageSize}";
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                   if (response.IsSuccessStatusCode)
                   {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        List<MovieDetails> movieD = JsonConvert.DeserializeObject<List<MovieDetails>>(jsonResponse);
                        List<Movie> movies = new List<Movie>();
                        foreach (var item in movieD)
                        {
                            Movie movie = new Movie
                            {
                                Title = item.Title,
                                Year = item.Year,
                                ImdbID = item.ImdbID,
                                Type = item.Type
                            };
                            movies.Add(movie);

                        }

                        return movies;
                    }
                    else
                    {
                        MessageBox.Show($"HTTP Error : {response.StatusCode}");
                        return new List<Movie>();
                    }
              }
              catch (HttpRequestException ex)
              {
                    MessageBox.Show(ex.Message);
                    return new List<Movie>();
              }
            }
        }


    private async void listMovies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            if (listMovies.SelectedItem != null)
            {
                Movie selectedMovie = (Movie)listMovies.SelectedItem;
                MovieDetailsWindow movieDetailsWindow = new MovieDetailsWindow(selectedMovie);
                movieDetailsWindow.ShowDialog();

                movies = await SearchMovies(textSearch.Text, currentPage, pageSize);
                if (movies.Count == 0)
                {
                    MessageBox.Show("No movies found.");
                }
                else
                {
                    listMovies.ItemsSource = movies;
                    btnNextPage.Visibility = movies.Count > 9 ? Visibility.Visible : Visibility.Collapsed;
                }

            }

        }

        public class Movie
        {
            [JsonProperty("title")]
            public string Title { get; set; }
            [JsonProperty("year")]
            public int Year { get; set; }
            [JsonProperty("imdbID")]
            public int ImdbID { get; set; }
            [JsonProperty("type")]
            public string Type { get; set; }
        }

        public class MovieDetails : Movie
        {
            [JsonProperty("rated")]
            public string Rated { get; set; }
            [JsonProperty("released")]
            public string Released { get; set; }
            [JsonProperty("runtime")]
            public string Runtime { get; set; }
            [JsonProperty("genre")]
            public string Genre { get; set; }
            [JsonProperty("director")]
            public string Director { get; set; }
            [JsonProperty("writer")]
            public string Writer { get; set; }
            [JsonProperty("actors")]
            public string Actors { get; set; }
            [JsonProperty("plot")]
            public string Plot { get; set; }
            [JsonProperty("poster")]
            public string Poster { get; set; }
            [JsonProperty("language")]
            public string Language { get; set; }
            [JsonProperty("rating")]
            public double Rating { get; set; }
            [JsonProperty("country")]
            public string Country { get; set; }
        }

        private void btnAddMovie_Click(object sender, RoutedEventArgs e)
        {
            AddMovie addMovie = new AddMovie();
            addMovie.ShowDialog();
        }

    }
}
