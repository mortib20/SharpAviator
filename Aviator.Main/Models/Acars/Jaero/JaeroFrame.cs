namespace Aviator.Main.Models.Acars.Jaero;

public class JaeroFrame
{
    public App app { get; set; }
    public Isu isu { get; set; }
    public string station { get; set; }
    public T t { get; set; }
}

public class App
{
    public string name { get; set; }
    public string ver { get; set; }
}

public class Isu
{
    public Acars acars { get; set; }
    public string qno { get; set; }
    public string refno { get; set; }
    public Dst dst { get; set; }
    public Src src { get; set; }
}

public class Acars
{
    public string ack { get; set; }
    public string blk_id { get; set; }
    public string label { get; set; }
    public string mode { get; set; }
    public string msg_text { get; set; }
    public string reg { get; set; }
}

public class Dst
{
    public string addr { get; set; }
    public string type { get; set; }
}

public class Src
{
    public string addr { get; set; }
    public string type { get; set; }
}

public class T
{
    public int sec { get; set; }
    public int usec { get; set; }
}

