namespace Firms.Models;
/*===========================================================================
    Класс типа подразделения содержит имя данного типа подразделений и инорма-
цию о том, является ли это подразделение основным.
===========================================================================*/
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

