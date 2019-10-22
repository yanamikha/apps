using System;
using System.Text.RegularExpressions;

namespace TheWeather.Controllers
{
    public partial class ValuesController
    {
        public static class Parse
        {
            public static string CurrentDay(string str)
            {

                Match match = Regex.Match(str, "<lastupdate value=\"(.*)T", RegexOptions.Singleline);
                string date = match.Groups[1].Value.Replace('-', '.');

                match = Regex.Match(str, "<temperature value=\"(.*?)\" ", RegexOptions.Singleline);

                double temperature = double.Parse(match.Groups[1].Value.Replace('.', ','));
                temperature -= 273.15;

                match = Regex.Match(str, "<wind><speed value=\"(.*?)\" unit=\"(.*?)\" name=\"(.*?)\">", RegexOptions.Singleline);
                string wind = $"Speed of wind: {match.Groups[1].Value} {match.Groups[2].Value}, wind is {match.Groups[3].Value}";

                match = Regex.Match(str, "<clouds value=\"(.*)\" name=\"(.*?)\"", RegexOptions.Singleline);
                string clouds = $"Cloud cover: {match.Groups[1].Value}, {match.Groups[2].Value}";
                return date + "\nTemperature: " + temperature + "°C\n" + wind + "\n" + clouds;
            }
            public static string FiveDaysForecast(string str)
            {
                MatchCollection matches = Regex.Matches(str, "<time from=\"(.*?)\"", RegexOptions.Singleline);
                string[] WeatherInfo = new string[matches.Count];

                for (int i = 0; i < matches.Count; i++)
                    WeatherInfo[i] = matches[i].Groups[1].Value.Replace('T', ' ').Replace('-','.') + "\n";

                matches = Regex.Matches(str, "min=\"(.*?)\" max=\"(.*?)\"", RegexOptions.Singleline);
                for (int i = 0; i < matches.Count; i++)
                {
                    double temperature = double.Parse(matches[i].Groups[1].Value.Replace('.', ','));
                    temperature -= 273.15;
                    WeatherInfo[i] += "Min temperature: " + Math.Round(temperature);

                    temperature = double.Parse(matches[i].Groups[2].Value.Replace('.', ','));
                    temperature -= 273.15;
                    WeatherInfo[i] += "°C max temperature: " + Math.Round(temperature) + " °C\n";
                }

                matches = Regex.Matches(str, "<windSpeed mps=\"(.*?)\" unit=\"(.*?)\" name=\"(.*?)\">", RegexOptions.Singleline);
                for (int i = 0; i < matches.Count; i++)
                    WeatherInfo[i] += $"speed of wind: {matches[i].Groups[1].Value} {matches[i].Groups[2].Value}, wind is {matches[i].Groups[3].Value}\n";

                matches = Regex.Matches(str, "<clouds value=\"(.*?)\" all=\"(.*?)\"", RegexOptions.Singleline);
                for (int i = 0; i < matches.Count; i++)
                    WeatherInfo[i] += $"Cloud cover: {matches[i].Groups[1].Value}, {matches[i].Groups[2].Value}%\n";
              
                string result = string.Empty;
                foreach (var m in WeatherInfo)
                {
                    result += m + "\n\n";
                }
                return result;
            }
        }

    }
}
