namespace WeazorLib.Models
{
	public class WeatherViewModel
	{
		public float Temperature { get; set; }
		public float TemperatureC { get => Temperature - 273.15f; }
		public float TemperatureF { get => (Temperature - 273.15f) * 9f / 5f + 32f; }
		public string WeatherType {  get; set; }
	}
}
