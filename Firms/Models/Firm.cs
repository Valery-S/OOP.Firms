namespace Firms.Models;
/*===========================================================================
    Класс фирмы. Содержит строго типизированные поля адресной информации и
словарь пользовательских значений - UserFields. Также содержит список
подразделений.
    Методы класса фирмы обеспечивают управление структурой фирмы и составом
ее контактов.
===========================================================================*/
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
    private Dictionary<string, string> _userFields=new();                       //Пользовательские поля
    public Dictionary<string, string> UserFields => new(_userFields);           //Копия словаря пользовательских полей
    private List<SubFirm> _subFirms = new();                                    //Подразделения фирмы
    public List<SubFirm> SubFirms => new(_subFirms);                            //Копия списка подразделений фирмы
    public int SubFirmsCount => _subFirms.Count;                                 //Количество подразделений

    private Firm() { }

    public Firm(string name, string shortName, string country, string region, string town, string street, string postIndex, string email, string web)
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
    }

    public void AddSubFirm(SubFirm subFirm)
        => _subFirms.Add(subFirm);

    public SubFirm GetMain() => _subFirms.First(x => x.SubFirmType.IsMain);

    public void AddContact(Contact contact)
    {
        var mainSubFirm = GetMain();
        mainSubFirm.AddContact(contact);
    }

    public void AddContactToSubFirm(Contact contact, SubFirmType subFirmType, bool checkOtherTypes = false)
    {
        var subFirm = _subFirms.FirstOrDefault(x => x.SubFirmType.Name == subFirmType.Name);

        if (subFirm is not null)
        {
            subFirm.AddContact(contact);
            return;
        }

        if (SubFirmsCount == 1 && checkOtherTypes)
        {
            this.AddContact(contact);
        }
    }

    public bool ExistContact(Contact contact)
        => _subFirms.Exists(sb => sb.ExistContact(contact));

    public void AddField(string name, string value)
    {
        _userFields.Add(name, value);
    }

    public void SetField(string name, string value)
        => _userFields[name] = value;

    public void RenameField(string oldName, string newName)
    {
        var data = _userFields[oldName];
        _userFields.Remove(oldName);
        _userFields.Add(newName, data);
    }

    public string GetField(string name)
        => _userFields[name];

}
