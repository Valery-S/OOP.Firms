namespace Firms.Models;

public class ContactType
{
    public string Name { get; private set; } = null!;
    public string Note { get; private set; } = null!;

    private ContactType() { }

    public ContactType(string name, string note)
    {
        Name = name;
        Note = note;
    }
}

