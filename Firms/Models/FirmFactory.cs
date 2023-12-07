using System.Xml.Linq;

namespace Firms.Models;
/*==================================================================================
    Класс фабрики фирм. Обеспечивает создание фирмы с переданными адресными данными,
и добавление ей основного подразделения. Реализует шаблон проектирования Singleton.
==================================================================================*/
public class FirmFactory
{
	private FirmFactory() { }
	private static FirmFactory? _factory = null;
	public static List<Firm> Firms { get; private set; } = new();
	public static SubFirmType MainSubFirmType { get; } = new(true, "Основной офис");

	public static FirmFactory Factory
	{
		get
		{
			_factory ??= new FirmFactory();
			return _factory;
		}
	}

    //Создание фирмы и главного подразделения
	public Firm Create(string name, string shortName, string country, string region, string town, string street,
		string postIndex, string email, string web)
	{
		var firm = new Firm(name, shortName, country, region, town, street,
			postIndex, email, web);

		firm.AddSubFirm(new SubFirm($"Основной офис фирмы {firm.Name}", $"ФИО начальника фирмы {firm.Name}",
            $"Полное имя начальника фирмы {firm.Name}", "Номер телефона","Электронная почта", MainSubFirmType));

		Firms.Add(firm);

		return firm;
	}
    
    //Добавление пользовательского поля фирме
    public void AddUserFieldForFirm(string firmName, string fieldName, string fieldValue)
    {
        var firms = Firms.Where(x => x.Name == firmName).ToList();
        if (firms.Count == 1)
        {
			firms[0].AddField(fieldName, fieldValue);
        }
    }
    //Получение копии словаря пользовательских полей фирмы
    public Dictionary<string, string> GetFirmFields(string firmName)
    {
        Dictionary<string, string> res=new ();
        var firms = Firms.Where(x => x.Name == firmName).ToList();
        if (firms.Count == 1)
        {
            res = firms[0].UserFields;
        }
        return res;
    }
    //Получение значения поля по его названию
    public string GetValueOfFieldByName(string firmName, string fieldName)
    {
        string res="";
        var firms = Firms.Where(x => x.Name == firmName).ToList();
        if (firms.Count == 1)
        {
            res = firms[0].GetField(fieldName);
        }
        return res;
    }
    //Изменение значения поля на новое
    public void SetValueOfFieldByName(string firmName, string fieldName, string fieldValue)
    {
        var firms = Firms.Where(x => x.Name == firmName).ToList();
        if (firms.Count == 1)
        {
           firms[0].SetField(fieldName, fieldValue);
        }
    }
    //Изменение названия пользовательского поля
    public void RenameFieldByName(string firmName, string oldFieldName, string newFieldName)
    {
        var firms = Firms.Where(x => x.Name == firmName).ToList();
        if (firms.Count == 1)
        {
            firms[0].RenameField(oldFieldName, newFieldName);
        }
    }
}