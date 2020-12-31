using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace QueryWeather.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private const string Host = "https://freecityid.market.alicloudapi.com";
        private const string Path = "/whapi/json/alicityweather/briefforecast3days";
        private const string Method = "POST";
        private const string AppCode = "70a488b8e5d14e4b8b18fa172b468996";

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public Models.AliCoinResponseDto Post(Models.AliCoinRequestDto data)
        {
            var result = new Models.AliCoinResponseDto();
            var _city = string.Empty;
            var _cityId = 0;
            var _dateStr = string.Empty;
            DateTime? _date = null;
            var _isNotQueryDateRang = false;
            var _weather = new Models.AliCityWeatherResult();
            data.SlotEntities.ForEach(x =>
            {
                if (x.IntentParameterName.Equals("city"))
                {
                    switch (x.StandardValue)
                    {
                        case "湘潭":
                            _city = "湘潭";
                            _cityId = 664;
                            break;
                        case "深圳":
                            _city = "深圳";
                            _cityId = 892;
                            break;
                    }
                }
                if (x.IntentParameterName.Equals("sys.date"))
                {
                    _dateStr = x.StandardValue;
                    switch (x.StandardValue)
                    {
                        case "今天":
                        case "今日":
                            _date = DateTime.Now;
                            break;
                        case "明天":
                        case "明日":
                            _date = DateTime.Now.AddDays(1);
                            break;
                        case "后天":
                        case "后日":
                            _date = DateTime.Now.AddDays(2);
                            break;
                        default:
                            _isNotQueryDateRang = true;
                            break;
                    }
                }
            });
            if (_isNotQueryDateRang)
            {
                result.ReturnValue = new Models.AliCoinResult
                {
                    Reply = $"当前仅能查询3天内的天气",
                    ExecuteCode = "SUCCESS"
                };
            }
            else
            {
                _weather = GetWeather(_cityId);
                result.ReturnCode = _weather.Code;
                if (_weather.Code == 0 && _weather.Data != null)
                {
                    var _tempWeather = _weather.Data.Forecast.Where(x => x.PredictDate.Date == _date.Value.Date).FirstOrDefault();
                    if (_tempWeather == null)
                    {
                        result.ReturnValue = new Models.AliCoinResult
                        {
                            Reply = $"未查询到{_city}{_dateStr}的天气",
                            ExecuteCode = "SUCCESS"
                        };
                    }
                    else
                    {
                        var _tempCondition = _tempWeather.ConditionDay.Equals(_tempWeather.ConditionNight) ? _tempWeather.ConditionDay : $"{_tempWeather.ConditionDay}转{_tempWeather.ConditionNight}";
                        result.ReturnValue = new Models.AliCoinResult
                        {
                            Reply = $"{_city}{_dateStr}，{_tempCondition}，气温{_tempWeather.TempNight}到{_tempWeather.TempDay}",
                            ResultType = "RESULT",
                            ExecuteCode = "SUCCESS"
                        };
                    }
                }
                else
                {
                    result.ReturnValue = new Models.AliCoinResult
                    {
                        Reply = $"未查询到{_city}的天气",
                        ExecuteCode = "SUCCESS"
                    };
                }
            }
            return result;
        }

        private Models.AliCityWeatherResult GetWeather(long cityId)
        {
            string querys = string.Empty;
            string bodys = $"cityId={cityId}&token=677282c2f1b3d718152c4e25ed434bc4";
            string url = $"{Host}{Path}";
            HttpWebRequest httpRequest = null;
            HttpWebResponse httpResponse = null;

            if (!string.IsNullOrEmpty(querys))
            {
                url = url + "?" + querys;
            }

            if (Host.IndexOf("https://") == 0)
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                httpRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
            }
            else
            {
                httpRequest = (HttpWebRequest)WebRequest.Create(url);
            }
            httpRequest.Method = Method;
            httpRequest.Headers.Add("Authorization", "APPCODE " + AppCode);
            //根据API的要求，定义相对应的Content-Type
            httpRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            if (!string.IsNullOrEmpty(bodys))
            {
                byte[] data = Encoding.UTF8.GetBytes(bodys);
                using (Stream stream = httpRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            try
            {
                httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            }
            catch (WebException ex)
            {
                httpResponse = (HttpWebResponse)ex.Response;
            }
            Stream st = httpResponse.GetResponseStream();
            StreamReader reader = new StreamReader(st, Encoding.GetEncoding("utf-8"));
            var _resContent = reader.ReadToEnd();

            var _result = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.AliCityWeatherResult>(_resContent);
            return _result;
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }
    }
}
