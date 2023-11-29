namespace Firms.Models;

public class Firm
{
    public string Name { get; private set; } = null!;                           //Полное наименование фирмы
    public string ShortName { get; private set; } = null!;                      //Краткое наименование фирмы
    public string Country { get; private set; } = null!;                        //Страна
    public string Region { get; private set; } = null!;                         //Регион (область)
    public string Town { get; private set; } = null!;                           //Город
    public string Street { get; private set; } = null!;                         //Улица
    public string PostIndex { get; private set; } = null!;                      //Почтовый индекс
    public DateTime DateIn { get; private set; } = DateTime.Now;                //Дата ввода фирмы (начало взаимоотношений)
    public string Email { get; private set; } = null!;                          //Почтовый адрес фирмы
    public string Web { get; private set; } = null!;                            //URL-адрес сайта
    public Dictionary<string, string> UserFields { get; private set; }= null!;  //Пользовательские поля
    public List<SubFirm> SubFirms { get; private set; } = new();                //Подразделения фирмы

    private Firm() { }

    public Firm(string name, string shortName, string country, string region, string town, string street, string postIndex, string email, string web, Dictionary<string, string>? fields = null)
    {
        Name = name;
        ShortName = shortName;
        Country = country;
        Region = region;
        Town = town;
        Street = street;
        PostIndex = postIndex;
        Email = email;
        Web = web;
        UserFields = fields ?? new Dictionary<string, string>();
    }

    public void AddSubFirm(SubFirm subFirm)
        => SubFirms.Add(subFirm);

    public int SubFirmsCount => SubFirms.Count;

    public SubFirm GetMain() => SubFirms.First(x => x.SubFirmType.IsMain);

    public void AddContact(Contact contact)
    {
        var mainSubFirm = GetMain();
        mainSubFirm.AddContact(contact);
    }

    public void AddContactToSubFirm(Contact contact, SubFirmType subFirmType, bool checkOtherTypes = false)
    {
        var subFirm = SubFirms.FirstOrDefault(x => x.SubFirmType == subFirmType);

        if (subFirm is not null)
        {
            subFirm.AddContact(contact);
            return;
        }

        if (SubFirms.Count == 1 && checkOtherTypes)
            this.AddContact(contact);
    }

    public bool ExistContact(Contact contact)
        => SubFirms.Exists(sb => sb.ExistContact(contact));

    public void SetField(string name, string value)
        => UserFields[name] = value;

    public void RenameField(string oldName, string newName)
    {
        var data = UserFields[oldName];
        UserFields.Remove(oldName);
        UserFields.Add(newName, data);
    }

    public string GetField(string name)
        => UserFields[name];

}
