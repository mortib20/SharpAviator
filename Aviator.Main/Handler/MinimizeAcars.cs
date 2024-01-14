using Aviator.Main.Models.Acars;
using Aviator.Main.Models.Acars.Acarsdec;
using Aviator.Main.Models.Acars.DumpVDL2;
using Aviator.Main.Models.Acars.Jaero;

namespace Aviator.Main.Handler;

public class MinimizeAcars
{
    public AcarsFrame? Handle(object data)
    {
        if (data is JaeroFrame jaero)
            return new AcarsFrame
            {
                Channel = jaero.app.ver,
                Label = jaero.isu.acars.label,
                Mode = jaero.isu.acars.mode,
                Text = jaero.isu.acars.msg_text,
                Registration = jaero.isu.acars.reg,
                Time = new DateTime(jaero.t.sec),
                Extras = new Dictionary<string, object?>
                {
                    ["Dst"] = jaero.isu.dst,
                    ["Src"] = jaero.isu.src
                }
            };

        if (data is DumpVdl2Frame dumpVdl2Frame)
        {
            if (dumpVdl2Frame.vdl2.avlc.acars is null) return null;

            return new AcarsFrame
            {
                Channel = dumpVdl2Frame.vdl2.freq.ToString(),
                Label = dumpVdl2Frame.vdl2.avlc.acars.label,
                Mode = dumpVdl2Frame.vdl2.avlc.acars.mode,
                Text = dumpVdl2Frame.vdl2.avlc.acars.msg_text,
                Flight = dumpVdl2Frame.vdl2.avlc.acars.flight,
                Registration = dumpVdl2Frame.vdl2.avlc.acars.reg,
                Time = default,
                Extras = new Dictionary<string, object?>
                {
                    ["Dst"] = dumpVdl2Frame.vdl2.avlc.dst,
                    ["Src"] = dumpVdl2Frame.vdl2.avlc.src,
                    ["NoiseLevel"] = dumpVdl2Frame.vdl2.noise_level
                }
            };
        }

        if (data is AcarsdecFrame acarsdecFrame)
            return new AcarsFrame
            {
                Channel = $"{acarsdecFrame.freq * 1000000}",
                Label = acarsdecFrame.label,
                Mode = acarsdecFrame.mode,
                Text = acarsdecFrame.text,
                Flight = acarsdecFrame.flight,
                Registration = acarsdecFrame.tail,
                Time = default,
                Extras = new Dictionary<string, object?>
                {
                    ["Level"] = acarsdecFrame.level
                }
            };

        return null;
    }
}