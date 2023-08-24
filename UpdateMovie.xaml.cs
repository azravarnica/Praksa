using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
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
    /// Interaction logic for UpdateMovie.xaml
    /// </summary>
    public partial class UpdateMovie : Window
    {
        private const string baseApiUrl = "http://localhost:5148/api/Movie";
        private string messageError = "";
        private MovieDetails movieDetailsUpdate = new MovieDetails();
        public UpdateMovie(MovieDetails movieDetails)
        {
            InitializeComponent();
            movieDetailsUpdate = movieDetails;
            textTitle.Text = movieDetails.Title;
            textYear.Text = movieDetails.Year.ToString();
            textType.Text = movieDetails.Type;
            textRated.Text = movieDetails.Rated;
            textReleased.Text = movieDetails.Released;
            textRuntime.Text = movieDetails.Runtime;
            textDirector.Text = movieDetails.Director;
            textPlot.Text = movieDetails.Plot;
            textLanguage.Text = movieDetails.Language;
            textWriter.Text = movieDetails.Writer;
            textCountry.Text = movieDetails.Country;
            textRating.Text = movieDetails.Rating.ToString();
            textGenre.Text = movieDetails.Genre;
            textActors.Text = movieDetails.Actors;
            textPoster.Text = movieDetails.Poster;
            textCountry.Text = movieDetails.Country;
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
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
                movieDetailsUpdate.Title = textTitle.Text.ToString();
                movieDetailsUpdate.Type = textType.Text.ToString();
                movieDetailsUpdate.Year = int.Parse(textYear.Text);
                movieDetailsUpdate.Rated = textRated.Text.ToString();
                movieDetailsUpdate.Released = textReleased.Text.ToString();
                movieDetailsUpdate.Runtime = textRuntime.Text.ToString();
                movieDetailsUpdate.Genre = textGenre.Text.ToString();
                movieDetailsUpdate.Director = textDirector.Text.ToString();
                movieDetailsUpdate.Writer = textWriter.Text.ToString();
                movieDetailsUpdate.Actors = textActors.Text.ToString();
                movieDetailsUpdate.Plot = textPlot.Text.ToString();
                movieDetailsUpdate.Poster = textPoster.Text.ToString();
                movieDetailsUpdate.Language = textLanguage.Text.ToString();
                movieDetailsUpdate.Country = textCountry.Text.ToString();
                movieDetailsUpdate.Rating = float.Parse(textRating.Text);

                bool isSuccess = await UpdateAndAddMovieToDatabase(movieDetailsUpdate);
                if (isSuccess)
                {
                    MessageBox.Show($"Successfully updated movie");
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"HTTP Error : {messageError}");
                }
            }
        }

        private async Task<bool> UpdateAndAddMovieToDatabase(MovieDetails movieDetails)
        {
            string jsonRequest = JsonConvert.SerializeObject(movieDetails, Formatting.Indented);
            string apiUrl = $"{baseApiUrl}/{movieDetails.ImdbID}";

            using (var hattpClient = new HttpClient())
            {
                try
                {
                    var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await hattpClient.PutAsync(apiUrl, content);

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
