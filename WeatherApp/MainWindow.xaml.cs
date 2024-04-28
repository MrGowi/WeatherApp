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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace WeatherApp
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly string apiKey = "ad1b7fda11ae3d548c15e3c1911937eb";

        private string requestUrl = "https://api.openweathermap.org/data/2.5/weather";

        public MainWindow()
        {
            InitializeComponent();

            UpdateData("Basel");

        }

        public void UpdateData(string city)
        {
            WeahterMapResponse result = GetWeatherData(city);

            string finalImage = "sun.png";
            string currentWeather = result.weather[0].main.ToLower();


            if (currentWeather.Contains("cloud"))
            {
                finalImage = "Cloud.png";
            }
            else if (currentWeather.Contains("rain"))
            {
                finalImage = "Rain.png";
            }
            else if (currentWeather.Contains("snow"))
            {
                finalImage = "Snow.png";
            }

            backgroundImage.ImageSource = new BitmapImage(new Uri("Images/" + finalImage, UriKind.Relative));

            labelTemperature.Content = result.main.temp.ToString("F1") + "°C";
            labelInfo.Content = result.weather[0].main;
        }

        public WeahterMapResponse GetWeatherData(string city)
        {
            HttpClient httpClient = new HttpClient();
            var finalUri = requestUrl + "?q=" + city + "&appid=" + apiKey + "&units=metric";
            HttpResponseMessage httpResponse = httpClient.GetAsync(finalUri).Result;
            string response = httpResponse.Content.ReadAsStringAsync().Result; 
            WeahterMapResponse weahterMapResponse = JsonConvert.DeserializeObject<WeahterMapResponse>(response); //Wir Deserialisiseren den Json String "response" in einen "WeahterMapResponse"
            return weahterMapResponse;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string query = textBoxQuery.Text;

            UpdateData(query); 

        }
    }
}
