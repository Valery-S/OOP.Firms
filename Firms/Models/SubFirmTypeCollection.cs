using System.Collections;

namespace Firms.Models;
/*=============================================================================
    Класс коллекции типов подразделений обеспечивают знание доступных типов под-
разделений и содержит методы для обхода.
=============================================================================*/
public class SubFirmTypeCollection : IEnumerable<SubFirmType>
{
	private readonly List<SubFirmType> _lst = new();
    public int Count => _lst.Count;

    public SubFirmTypeCollection(List<SubFirmType> value)
	{
		_lst = value;
	}
	
	public void Add(SubFirmType type)
		=> _lst.Add(type);

	public void Clear() => _lst.Clear();

	public IEnumerator<SubFirmType> GetEnumerator()
		=> _lst.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}