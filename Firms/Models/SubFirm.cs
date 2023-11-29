namespace Firms.Models;
/*=============================================================================
    Класс подразделения содержит строго типизированные поля контактной информа-
ции подразделения и список собственных контактов.
   Методы класса подразделения обеспечивают управление составом контактов и
принадлежности подразделения к заданному типу.
=============================================================================*/
public class SubFirm 
{
    public string Name { get; private set; } = null!;               //Наименование подразделения
    public string BossName { get; private set; } = null!;           //Имя руководителя подразделения
    public string OfficialBossName { get; private set; } = null!;   //Официальное обращение к руководителю
    public string Phone { get; private set; } = null!;              //номер телефона подразделения
    public string Email { get; private set; } = null!;              //Почтовый адрес подразделения
    public SubFirmType SubFirmType { get; private set; } = null!;   //Тип подразделения
    private List<Contact> _contacts = new();                        //Контакты подразделения
    public List<Contact> Contacts => new(_contacts);

    private SubFirm()
    { }

    public SubFirm(string name, string bossName, string ofcBossName, string tel, string email, SubFirmType subFirmType)
    {
        Name = name;
        BossName = bossName;
        OfficialBossName = ofcBossName;
        Phone = tel;
        Email = email;
        SubFirmType = subFirmType;
    }

    public void AddContact(Contact contact)
        => _contacts.Add(contact.Clone());

    public bool ExistContact(Contact contact)
        => _contacts.Exists(x => x == contact);

    public bool IsYourType(SubFirmType type)
        => this.SubFirmType.Name == type.Name;

}

