using System;
using System.Collections.Generic;

namespace QueryWeather.Models
{
    public class AliCityWeatherResult
    {
        /// <summary>
        /// 0默认表示成功
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 天气预报
        /// </summary>
        public AliCityWeather Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Msg { get; set; }
    }

    public class AliCityWeather
    {
        /// <summary>
        /// 城市
        /// </summary>
        public City City { get; set; }

        /// <summary>
        /// 精简预报
        /// </summary>
        public List<Forecast> Forecast { get; set; }
    }

    public class City
    {
        /// <summary>
        /// 城市Id
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// 国家名称
        /// </summary>
        public string CounName { get; set; }

        /// <summary>
        /// 城市名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 上级区域名称(省名)
        /// </summary>
        public string Pname { get; set; }
    }

    public class Forecast
    {
        /// <summary>
        /// 白天天气
        /// </summary>
        public string ConditionDay { get; set; }

        /// <summary>
        /// 白天天气Id
        /// </summary>
        public string ConditionIdDay { get; set; }

        /// <summary>
        /// 夜间天气Id
        /// </summary>
        public string ConditionIdNight { get; set; }

        /// <summary>
        /// 夜间天气
        /// </summary>
        public string ConditionNight { get; set; }

        /// <summary>
        /// 预报日期
        /// </summary>
        public DateTime PredictDate { get; set; }

        /// <summary>
        /// 白天温度
        /// </summary>
        public string TempDay { get; set; }

        /// <summary>
        /// 夜间温度
        /// </summary>
        public string TempNight { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime Updatetime { get; set; }

        /// <summary>
        /// 白天风向
        /// </summary>
        public string WindDirDay { get; set; }

        /// <summary>
        /// 夜间风向
        /// </summary>
        public string WindDirNight { get; set; }

        /// <summary>
        /// 白天风级
        /// </summary>
        public string WindLevelDay { get; set; }

        /// <summary>
        /// 夜间风级
        /// </summary>
        public string WindLevelNight { get; set; }
    }
}
