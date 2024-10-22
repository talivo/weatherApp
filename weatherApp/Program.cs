using System;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace weatherApp
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isRunning = true;
            while (isRunning)
            {  
                Console.WriteLine("Voer een willekeurige stad in");
                string city = Console.ReadLine();
                string apiKey = "3967ff45d7690e4d54db7f0be2792273";
                var client = new RestClient($"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}");
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    JObject jsonResponse = JObject.Parse(response.Content);
                    double temperatureInKelvin = (double)jsonResponse["main"]["temp"];
                    int temperatureInCelsius = Convert.ToInt32(temperatureInKelvin - 273.15); 
                    
                    string weatherDescription = (string)jsonResponse["weather"][0]["description"];

                    Console.WriteLine($"Weer in {city}:");
                    Console.WriteLine($"Temperatuur: {temperatureInCelsius}°C");
                    Console.WriteLine($"Beschrijving: {weatherDescription}");
                }
                else
                {
                    Console.WriteLine("Error bij het ophalen van data.");
                    Console.WriteLine(response.ErrorMessage);
                }
                Console.WriteLine("Voer X in om opnieuw te gaan");
                string continueOrTerminate = Console.ReadLine();
                if (continueOrTerminate == "X" || continueOrTerminate == "x")
                {
                    isRunning = true;
                } else
                {
                    Console.WriteLine("Programma wordt afgesloten...");
                    isRunning = false;
                }
            }
        }
    }
}
