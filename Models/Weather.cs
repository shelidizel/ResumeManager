// Top-level class to represent the entire JSON response
using Newtonsoft.Json;

public class WeatherApiResponse
{
    public List<HourData> hours { get; set; }
    public MetaData meta { get; set; }
}

// Class to represent a single data point in the 'hours' array
public class HourData
{
    public AirTemperature airTemperature { get; set; }
    public string time { get; set; }

    // Add other parameters if you request them (e.g., waveHeight, swellHeight)
    // public WaveHeight waveHeight { get; set; }
    // public SwellHeight swellHeight { get; set; }
}

// Class to represent the 'airTemperature' object
public class AirTemperature
{
    [JsonProperty("ecmwf:aifs")]
    public double? ecmwf { get; set; }
    public double? ecmwf_aifs { get; set; } // Note the name change
    public double? noaa { get; set; }
    public double? sg { get; set; }
}

// Class to represent the 'meta' object
public class MetaData
{
    public int cost { get; set; }
    public int dailyQuota { get; set; }
    public string end { get; set; }
    public double lat { get; set; }
    public double lng { get; set; }
    public List<string> @params { get; set; }
    public int requestCount { get; set; }
    public string start { get; set; }
}