using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace SearchFilmsService
{
    public class MovieService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public MovieService(string apiKey)
        {
            _httpClient = new HttpClient();
            _apiKey = apiKey;
        }

        public async Task<string> GetMovieInfo(string movieTitle)
        {
            try
            {
                string url = $"http://www.omdbapi.com/?apikey={_apiKey}&t={movieTitle}";
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    dynamic data = JObject.Parse(json);

                    if (data["Response"] == "True")
                    {
                        string title = data["Title"];
                        string year = data["Year"];
                        string genre = data["Genre"];
                        string director = data["Director"];
                        string plot = data["Plot"];
                        string rating = data["imdbRating"];

                        string movieInfo = $"Название: {title}\n" +
                                           $"Год: {year}\n" +
                                           $"Жанр: {genre}\n" +
                                           $"Режиссер: {director}\n" +
                                           $"Описание: {plot}\n" +
                                           $"IMDb рейтинг: {rating}\n";

                        return movieInfo;
                    }
                    else
                    {
                        return "Фильм не найден";
                    }
                }
                else
                {
                    return "Ошибка при получении информации о фильме";
                }
            }
            catch (Exception ex)
            {
                return "Ошибка: " + ex.Message;
            }
        }
    }
}
