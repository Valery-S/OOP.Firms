namespace Firms.Models;
/*=============================================================================
    Класс контакта представляет собой любое взаимодействие фирмы владельца с 
подразделением другой фирмы и он содержит строго типизированные поля, соде-
ржащие информацию о контакте.
    Методы класса контакта обеспечивают клонирование контакта и переопреде-
ляют операции равенства и неравенства контактов.
=============================================================================*/
public class Contact
{
    public DateTime BeginDt { get; private set; } = DateTime.Now;           //Дата начала контакта
    public DateTime EndDt { get; private set; } = DateTime.Now.AddYears(1); //Дата окнчания контакта
    public string Description { get; private set; } = null!;                //Описание контакта для себя
    public string DataInfo { get; private set; } = null!;                   //Формулировка контакта для клиента
    public ContactType ContactType { get; private set; } = null!;           //Вид контакта

    private Contact() { }

    public Contact(string description, string dataInfo, ContactType contactType, DateTime? beginDate = null, DateTime? endDate = null)
    {
        Description = description;
        DataInfo = dataInfo;
        ContactType = contactType;
        BeginDt = beginDate ?? default;
        EndDt = endDate ?? default;
    }

    public Contact Clone() => new(Description, DataInfo, ContactType, BeginDt, EndDt);
    
    public static bool operator ==(Contact left, Contact right)
    {
        return
            (left.Description, left.ContactType.Name, left.ContactType.Note, left.DataInfo)
            == (right.Description, right.ContactType.Name, right.ContactType.Note, right.DataInfo);
    }

    public static bool operator !=(Contact left, Contact right)
    {
        return !(left == right);
    }
}

