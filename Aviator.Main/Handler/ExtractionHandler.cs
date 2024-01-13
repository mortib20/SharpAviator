using Aviator.Main.Models.Acars;
using Aviator.Main.Models.Acars.Acarsdec;
using Aviator.Main.Models.Acars.DumpVDL2;
using Aviator.Main.Models.Acars.Jaero;

namespace Aviator.Main.Handler;

public class ExtractionHandler : AbstractHandler
{
    public override object? Handle(object data)
    {
        if (data is JaeroFrame jaero)
        {
            return new AcarsFrame
            {
                Channel = jaero.app.ver,
                Label = jaero.isu.acars.label,
                Text = jaero.isu.acars.msg_text,
            };
        }

        if (data is DumpVdl2Frame dumpVdl2Frame)
        {
            return new AcarsFrame
            {
                Channel = dumpVdl2Frame.vdl2.freq.ToString(),
                Label = dumpVdl2Frame.vdl2.avlc.acars?.label ?? string.Empty,
                Text = dumpVdl2Frame.vdl2.avlc.acars?.msg_text ?? string.Empty,
            };
        }

        if (data is AcarsdecFrame acarsdecFrame)
        {
            return new AcarsFrame
            {
                Channel = $"{acarsdecFrame.freq * 1000000}",
                Label = acarsdecFrame.label,
                Text = acarsdecFrame.text
            };
        }

        return null;
    }
}