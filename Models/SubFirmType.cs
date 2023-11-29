namespace Firms.Models;

public class SubFirmType
{
    public bool IsMain { get; private set; }
    public string Name { get; private set; } = null!;

    private SubFirmType() { }

    public SubFirmType(bool isMain, string name)
    {
        IsMain = isMain;
        Name = name;
    }
}

