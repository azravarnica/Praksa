using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Newtonsoft.Json.Linq;

namespace MovieSearchApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string apiKey = "64d8d178";
        private const string baseApiUrl = "https://www.omdbapi.com/";
        public MainWindow()
        {
            InitializeComponent();
        }

        private int currentPage = 1;
        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            currentPage = 1;
            string searchText = textSearch.Text;
            if (string.IsNullOrEmpty(searchText))
            {
                MessageBox.Show("Enter a search term!");
                return;

            }
            else if(searchText.Length > 8)
            {
                MessageBox.Show("Term is too long!");
                return;
            }
            string apiUrl = $"{baseApiUrl}?apikey={apiKey}&s={searchText}&page={currentPage}";
            await SearchMovies(apiUrl);
            btnNextPage.IsEnabled = (currentPage * 10) < totalResults;
        }

        private async void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            currentPage++;
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
            string apiUrl = $"{baseApiUrl}?apikey={apiKey}&s={searchText}&page={currentPage}";
            await SearchMovies(apiUrl);
            btnNextPage.IsEnabled = (currentPage * 10) < totalResults;

        }
        private int totalResults = 0;

        private async Task SearchMovies(string apiUrl)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        JObject jsonBody = JObject.Parse(jsonResponse);
                        if (jsonBody["Response"].ToString().ToLower() == "true")
                        {
                            JArray movieArray = JArray.Parse(jsonBody["Search"].ToString());
                            totalResults = int.Parse(jsonBody["totalResults"].ToString());
                            listMovies.Items.Clear();
                            List<Movie> movies = new List<Movie>();
                            foreach (var item in movieArray)
                            {
                                Movie movie = new Movie
                                {
                                    Title = item["Title"].ToString(),
                                    Year = item["Year"].ToString(),
                                    ImdbID = item["imdbID"].ToString(),
                                    Type = item["Type"].ToString()
                                };
                                movies.Add(movie);
                            }
                            listMovies.ItemsSource = movies;
                        }
                        else
                        {
                            MessageBox.Show("No results found for this term");
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show($"HTTP Error : {response.StatusCode}");
                    }
                } catch (HttpRequestException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private async void listMovies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            if (listMovies.SelectedItem != null)
            {
                Movie selectedMovie = (Movie)listMovies.SelectedItem;
                string selectedMovieId = selectedMovie.ImdbID;
                string apiUrl = $"{baseApiUrl}?apikey={apiKey}&i={selectedMovieId}";
                JObject movieDetails = await GetMovieDetails(apiUrl);
                MovieDetailsWindow movieDetailsWindow = new MovieDetailsWindow(movieDetails);
                movieDetailsWindow.ShowDialog();
            }

        }

        private async Task<JObject> GetMovieDetails(string apiUrl)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        JObject jsonBody = JObject.Parse(jsonResponse);
                        return jsonBody;
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
            return null;
        }

        public class Movie
        {
            public string Title { get; set; }
            public string Year { get; set; }
            public string ImdbID { get; set; }
            public string Type { get; set; }

        }

    }
}
