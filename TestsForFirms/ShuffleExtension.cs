public static class ShuffleExtension
{
	public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> items)
	{
		if (items == null) throw new ArgumentNullException(nameof(items));

		var rnd = new Random();

		return items.OrderBy(_ => rnd.Next());
	}
}