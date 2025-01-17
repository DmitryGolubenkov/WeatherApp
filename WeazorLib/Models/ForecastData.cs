﻿using System.Collections.Generic;

namespace WeazorLib.Models
{
    public class ForecastData
    {
        public string cod { get; set; }
        public int message { get; set; }
        public int cnt { get; set; }
        public List<List> list { get; set; }
        public City city { get; set; }
    }
}
