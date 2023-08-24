using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
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
using static MovieSearchApp.MainWindow;

namespace MovieSearchApp
{
    /// <summary>
    /// Interaction logic for AddAndUpdateMovie.xaml
    /// </summary>
    public partial class AddMovie : Window
    {
        private const string baseApiUrl = "http://localhost:5148/api/Movie";
        private string messageError = "";

        public AddMovie()
        {
            InitializeComponent();
        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textYear.Text) || string.IsNullOrWhiteSpace(textRating.Text)
                || string.IsNullOrWhiteSpace(textTitle.Text) || string.IsNullOrWhiteSpace(textType.Text)
                || string.IsNullOrWhiteSpace(textReleased.Text) || string.IsNullOrWhiteSpace(textRated.Text)
                || string.IsNullOrWhiteSpace(textRuntime.Text) || string.IsNullOrWhiteSpace(textGenre.Text)
                || string.IsNullOrWhiteSpace(textDirector.Text) || string.IsNullOrWhiteSpace(textWriter.Text)
                || string.IsNullOrWhiteSpace(textActors.Text) || string.IsNullOrWhiteSpace(textPlot.Text)
                || string.IsNullOrWhiteSpace(textPoster.Text) || string.IsNullOrWhiteSpace(textLanguage.Text)
                || string.IsNullOrWhiteSpace(textCountry.Text))
            {
                MessageBox.Show("All details must be filled in.");
            }
            else 
            { 

            MovieDetails movieDetails = new MovieDetails
            {
                Title = textTitle.Text.ToString(),
                Type = textType.Text.ToString(),
                Year = int.Parse(textYear.Text),
                Rated = textRated.Text.ToString(),
                Released = textReleased.Text.ToString(),
                Runtime = textRuntime.Text.ToString(),
                Genre = textGenre.Text.ToString(),
                Director = textDirector.Text.ToString(),
                Writer = textWriter.Text.ToString(),
                Actors = textActors.Text.ToString(),
                Plot = textPlot.Text.ToString(),
                Poster = textPoster.Text.ToString(),
                Language = textLanguage.Text.ToString(),
                Country = textCountry.Text.ToString(),
                Rating = (float)Math.Round(float.Parse(textRating.Text), 1)
            };

            bool isSuccess = await AddMovieToDatabase(movieDetails);
               if (isSuccess)
               {
                MessageBox.Show($"Successfully added movie");
               }
               else
               {
                MessageBox.Show($"HTTP Error : {messageError}");
               }
            }
        }

        private async Task<bool> AddMovieToDatabase(MovieDetails movieDetails)
        {
            string jsonRequest = JsonConvert.SerializeObject(movieDetails, Formatting.Indented);

            using (var hattpClient = new HttpClient())
            {
                try
                {
                    var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await hattpClient.PostAsync(baseApiUrl, content);

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
