namespace WeazorLib.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

    public class Sys
    {
        public int? type { get; set; }
        public int? id { get; set; }
        public string? country { get; set; }
        public int? sunrise { get; set; }
        public int? sunset { get; set; }

        public string? pod { get; set; }
    }


}
