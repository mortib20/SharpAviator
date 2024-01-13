using System.ComponentModel.DataAnnotations;

namespace Aviator.Main.Models.Acars;

public class AcarsFrame
{
    public string Channel { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
}