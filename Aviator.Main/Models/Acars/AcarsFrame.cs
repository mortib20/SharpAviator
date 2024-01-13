namespace Aviator.Main.Models.Acars;

public class AcarsFrame
{
    public string? Channel { get; set; }
    public string? Label { get; set; }
    public string? Mode { get; set; }
    public string? Text { get; set; }
    public string? Flight { get; set; }
    public string? Registration { get; set; }
    public DateTime Time { get; set; }
    public Dictionary<string, object?>? Extras { get; set; }
}