using System.Collections;

namespace Firms.Models;

public class SubFirmTypeCollection : IEnumerable<SubFirmType>
{
	private readonly List<SubFirmType> _lst = new();

	public SubFirmTypeCollection(List<SubFirmType> value)
	{
		_lst = value;
	}

	public int Count => _lst.Count;

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