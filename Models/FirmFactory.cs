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

	public Firm Create(string name, string shortName, string country, string region, string town, string street,
		string postIndex, string email, string web, Dictionary<string, string>? fields = null)
	{
		var firm = new Firm(name, shortName, country, region, town, street,
			postIndex, email, web, fields);

		firm.AddSubFirm(new SubFirm($"Основной офис фирмы {firm.Name}", $"ФИО начальника фирмы {firm.Name}", 
            "Полное имя начальника фирмы","Номер телефона","Электронная почта", MainSubFirmType));

		Firms.Add(firm);

		return firm;
	}
}