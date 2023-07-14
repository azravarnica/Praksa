using Newtonsoft.Json;
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
using System.IO;

namespace PrviTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> myList;
        public MainWindow()
        {
            InitializeComponent();
            myList = new List<string> { "Azra voli more", "Azra voli teretanu", "Azra voli planinarenje", "Azra ne voli da vozi automobil" };
        }

        private void serializeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string json = JsonConvert.SerializeObject(myList, Formatting.Indented);
                string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "InformacijeAzrine.json");
                File.WriteAllText(filePath, json);

                MessageBox.Show("Serijalizacija uspješna!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom serijalizacije: " + ex.Message);
            }
        }

        private void deserializeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "InformacijeAzrine.json");
                if (File.Exists(filePath))
                {
                    List<string> deserializedList = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(filePath));
                    MessageBox.Show(string.Join(Environment.NewLine, deserializedList));
                    //MessageBox.Show("Deserijalizacija uspješna!");
                }
                else
                {
                    MessageBox.Show("Fajl ne postoji.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom deserijalizacije: " + ex.Message);
            }
        }
    }
}
