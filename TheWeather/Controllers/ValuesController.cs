using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

namespace TheWeather.Controllers
{
    [Route("api")]
    [ApiController]

    public partial class ValuesController : ControllerBase
    {
        // GET api/GetCurrentWeather
        [HttpGet("GetCurrentWeather")]
        public string GetCurrentWeather(string city)
        {
            string line = string.Empty;
            using (WebClient wc = new WebClient())
            {
                try { line = wc.DownloadString("https://api.openweathermap.org/data/2.5/weather?q=" + city + "&mode=xml&appid=58674fffe2de0c19d2c847c9cb4d01a9"); }
                catch { return "Incorrect city"; }
                if (System.Text.RegularExpressions.Regex.IsMatch(city, @"[a-z\-]$"))
                    return Parse.CurrentDay(line);               
               return "Incorrect city";
            }
        }

        // GET api/GetForecast
        [HttpGet("GetForecast")]
        public string GetForecast(string city)
        {
         
            string line = string.Empty;
            using (WebClient wc = new WebClient())
            {
                try { line = wc.DownloadString("https://api.openweathermap.org/data/2.5/forecast?q=" + city + "&mode=xml&appid=58674fffe2de0c19d2c847c9cb4d01a9"); }
                catch { return "Incorrect city"; }
                if (System.Text.RegularExpressions.Regex.IsMatch(city, @"[a-z\-]$"))
                    return Parse.FiveDaysForecast(line);
                return "Incorrect city";
            }
        }

    }
}

