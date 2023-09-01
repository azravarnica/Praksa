using Newtonsoft.Json;
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
    /// Interaction logic for LoginUser.xaml
    /// </summary>
    public partial class LoginUser : Window
    {
        private const string baseApiUrl = "http://localhost:5148/api/UserLogins";
        private User user;
        public LoginUser()
        {
            InitializeComponent();
            imgLogo.Source = new BitmapImage(new Uri("login.jpeg", UriKind.Relative));
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            user = await SearchUser(txtUsername.Text, passBox.Password);
            if(txtUsername.Text == user.Name && passBox.Password == user.Password)
            {
                MainWindow mainWindow = new MainWindow();
                Application.Current.MainWindow = mainWindow;
                this.Close();
                mainWindow.Show();
            }
            else
            {
                MessageBox.Show("Invalid credentials. Please try again.");
            }
        }

        private async Task<User> SearchUser(string username, string password)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    string apiUrl = $"{baseApiUrl}/search?userName={username}&passwordUser={password}";
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        user = JsonConvert.DeserializeObject<User>(jsonResponse);
                    return user;

                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show(ex.Message);
                    return user;
                }
            }
        }
    }
}
