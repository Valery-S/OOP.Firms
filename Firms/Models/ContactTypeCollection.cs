using System.Collections;

namespace Firms.Models;
/*=============================================================================
    Класс коллекции типов контактов  обеспечивают знание доступных типов контак-
тов и содержит методы для обхода.
=============================================================================*/
public class ContactTypeCollection : IEnumerable<ContactType>
{
	private readonly List<ContactType> _lst = new();
    public int Count => _lst.Count;

    public ContactTypeCollection(List<ContactType> value)
	{
		_lst = value;
	}
	
	public void Add(ContactType type)
		=> _lst.Add(type);

	public void Clear() => _lst.Clear();

	public IEnumerator<ContactType> GetEnumerator()
		=> _lst.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}