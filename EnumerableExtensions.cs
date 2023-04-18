/// <summary>
///   Contains extension methods for <see cref="IEnumerable{TItem}" />.
/// </summary>
public static class EnumerableExtensions
{
	/// <summary>
	///   Transforms an enumerable into a one to one map.
	/// </summary>
	/// <typeparam name="TItem">The type of items of original enumerable.</typeparam>
	/// <typeparam name="TKey">The type of key of one to one map.</typeparam>
	/// <typeparam name="TValue">The type of value of one to one map.</typeparam>
	/// <param name="enumerable">The original enumerable which has to be transformed.</param>
	/// <param name="keySelector">The function, according to which the keys of one to one map are selected.</param>
	/// <param name="valueSelector">The function, according to which the values of one to one map are selected.</param>
	/// <returns></returns>
	public static Map<TKey, TValue> ToOneToOneMap<TItem, TKey, TValue>(this IEnumerable<TItem> enumerable, Func<TItem, TKey> keySelector, Func<TItem, TValue> valueSelector)
	{
		Dictionary<TKey, TValue> dictionary = enumerable.ToDictionary(keySelector, valueSelector);
		return new Map<TKey, TValue>(dictionary, true);
	}
}
