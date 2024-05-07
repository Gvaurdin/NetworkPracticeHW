using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WeatherForecast
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private string _apiKey = "4a3f544c-5c6c-4e83-8ef5-0a56699c1621";

        public WeatherService(string apiKey)
        {
            _httpClient = new HttpClient();
            _apiKey = apiKey;
        }

        public async Task<string> GetWeather(string city)
        {
            try
            {
                string url = $"https://api.weather.yandex.ru/v2/forecast?lat=52.37125&lon=4.89388";
                _httpClient.DefaultRequestHeaders.Add("X-Yandex-Weather-Key", _apiKey);
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    // обработка JSON для получения прогноза на ближайшие семь дней
                    dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                    string weatherForecast = $"Прогноз погоды на ближайшие семь дней для города {city}:\n";
                    for (int i = 0; i < 7; i++)
                    {
                        string date = data["forecasts"][i]["date"];
                        string condition = data["forecasts"][i]["parts"]["day"]["condition"];
                        double temperature = data["forecasts"][i]["parts"]["day"]["temp_avg"];
                        double windSpeed = data["forecasts"][i]["parts"]["day"]["wind_speed"];
                        weatherForecast += $"{date}: {condition}, Температура: {temperature}°C, Скорость ветра: {windSpeed} м/с\n";
                    }
                    return weatherForecast;
                }
                else
                {
                    return "Ошибка при получении данных о погоде";
                }
            }
            catch (Exception ex)
            {
                return "Ошибка: " + ex.Message;
            }
        }
    }
}
