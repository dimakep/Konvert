using System;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Konvert.PostalData;
using static System.Net.Mime.MediaTypeNames;

namespace Konvert
{
    internal class PostalData
    {

        public static async Task Main()
        {
            
            // Ваш исходный код, который выполняет все действия
            await PerformActionsAsync();
        }

        public class PostalOfficeData
        {
            [JsonPropertyName("index")]
            public string Index { get; set; }

            [JsonPropertyName("opsname")]
            public string Opsname { get; set; }

            [JsonPropertyName("region")]
            public string Region { get; set; }

            [JsonPropertyName("city")]
            public string City { get; set; }

            [JsonPropertyName("area")]
            public string Area { get; set; }

            [JsonPropertyName("settlement")]
            public string Settlement { get; set; }
        }

        public class Suggestion
        {
            [JsonPropertyName("value")]
            public string Value { get; set; }
            [JsonPropertyName("unrestricted_value")]
            public string UnrestrictedValue { get; set; }
            [JsonPropertyName("data")]
            public PostalOfficeData Data { get; set; }
        }

        public class Response
        {
            [JsonPropertyName("suggestions")]
            public Suggestion[] Suggestions { get; set; }
        }

        public static async Task PerformActionsAsync()
        {

            var url = "http://suggestions.dadata.ru/suggestions/api/4_1/rs/findById/postal_office";
            var jsonContent = $"{{\"query\": \"{Variables.Index}\"}}";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Token 5e7c1abc30aa8426254f9572209bd6301dcfe17b");
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();

                        // Выводим полный ответ от сервера, чтобы точно увидеть, что приходит
                        Console.WriteLine("Ответ от сервера:");
                        Console.WriteLine(responseData);

                        var result = JsonSerializer.Deserialize<Response>(responseData);

                        if (result?.Suggestions != null && result.Suggestions.Length > 0)
                        {
                            var suggestion = result.Suggestions[0]; // Извлекаем первое предложение

                            string value = suggestion.Value;
                            string unrestrictedValue = suggestion.UnrestrictedValue;
                            string postalCode = suggestion.Data.Index;
                            string opsname = suggestion.Data.Opsname;
                            string region = suggestion.Data.Region.Split(' ')[0];
                            string city = suggestion.Data.City;
                            string area = suggestion.Data.Area.Split(' ')[0];
                            string settlement = suggestion.Data.Settlement ?? "Не указано";

                            Variables.Region = region;
                            Variables.Area = area;
                            Variables.City = city;
                        }
                        else
                        {
                            MessageBox2 messageBox2 = new("Не найдено?", "");
                            messageBox2.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBox2 messageBox2 = new($"Ошибка:", response.StatusCode.ToString());
                        messageBox2.ShowDialog();
                        var errorDetails = await response.Content.ReadAsStringAsync();

                        MessageBox2 messageBox3 = new($"Детали ошибки:",errorDetails);
                        messageBox3.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox2 messageBox2 = new($"Ошибка:", ex.Message.ToString());
                    messageBox2.ShowDialog();
                }
            }
        }
    }


}

