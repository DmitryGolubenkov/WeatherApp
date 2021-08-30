namespace WeazorLib
{
    public class TemperatureConverter
    {

        public float Temperature { get; set; }
        public float TemperatureC { get => Temperature - 273.15f; }
        public float TemperatureF { get => (Temperature - 273.15f) * 9f / 5f + 32f; }

        public static float ToCelsius(float tempKelvin) { 
            return tempKelvin - 273.15f;
        }

        public static float ToFahrenheit(float tempKelvin)
        {
            return (tempKelvin - 273.15f) * 9f / 5f + 32f;
        }
    }
}
