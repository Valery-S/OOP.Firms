using System.Collections;

namespace Firms.Models;

public class ContactTypeCollection : IEnumerable<ContactType>
{
	private readonly List<ContactType> _lst = new();

	public ContactTypeCollection(List<ContactType> value)
	{
		_lst = value;
	}

	public int Count => _lst.Count;

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