/// <summary>
///   Represents two way 1 to 1 dictionary.
/// </summary>
/// <typeparam name="TKey">Type of key of original dictionary.</typeparam>
/// <typeparam name="TValue">Type of value of original dictionary.</typeparam>
public class Map<TKey, TValue>
{
	private readonly Dictionary<TKey, TValue> original;
	private readonly Dictionary<TValue, TKey> reverse;

	/// <summary>
	///   Creates an instance of <see cref="Map{TKey,TValue}" />.
	/// </summary>
	/// <param name="dict">The original dictionary, from which 1 to 1 map should be done.</param>
	/// <param name="referenceOriginalDictionary">
	///   CAREFUL! If memory is important, and contents of original reference
	///   dictionary will never change, only then set to true. Otherwise, the behavior will not be proper.
	/// </param>
	public Map(IDictionary<TKey, TValue> dict, bool referenceOriginalDictionary = false)
	{
		original = referenceOriginalDictionary && dict is Dictionary<TKey, TValue> exactDict ? exactDict : dict.ToDictionary(pair => pair.Key, pair => pair.Value);
		reverse = dict.ToDictionary(pair => pair.Value, pair => pair.Key);
	}

	/// <summary>
	///   Gets original dictionary of this map.
	/// </summary>
	public IReadOnlyDictionary<TKey, TValue> Original => original;

	/// <summary>
	///   Gets reverse dictionary of this map.
	/// </summary>
	public IReadOnlyDictionary<TValue, TKey> Reverse => reverse;

	/// <summary>
	///   Adds a key and value pair into this map.
	/// </summary>
	/// <param name="key">The key which should be added.</param>
	/// <param name="value">The value which should be added.</param>
	public void Add(TKey key, TValue value)
	{
		original.Add(key, value);
		reverse.Add(value, key);
	}

	/// <summary>
	///   Removes a pair from this map.
	/// </summary>
	/// <param name="key">The key of the pair to be removed.</param>
	/// <param name="value">The value of the pair to be removed.</param>
	/// <returns>True if the pair was successfully removed from the map.</returns>
	public bool Remove(TKey key, TValue value)
	{
		bool wasRemovedOriginal = original.Remove(key);
		bool wasRemovedReverse = reverse.Remove(value);
		return wasRemovedOriginal || wasRemovedReverse;
	}

	/// <summary>
	///   Removes a pair from this map. Throws exception, if removal fails.
	/// </summary>
	/// <param name="key">The key of the pair to be removed.</param>
	/// <param name="value">The value of the pair to be removed.</param>
	public void MustRemove(TKey key, TValue value)
	{
		original.MustRemoveKey(key);
		reverse.MustRemoveKey(value);
	}
}

/// <summary>
///   Represents a two way 1 to many map between a key and multiple items (of a collection). One item must only have 1 key.
/// </summary>
/// <typeparam name="TKey">Type of keys of map.</typeparam>
/// <typeparam name="TItem">The type of items that are in collection.</typeparam>
/// <typeparam name="TCollection">The type of collection of map.</typeparam>
public class Map<TKey, TItem, TCollection>
	where TCollection : ICollection<TItem>, new()
{
	private readonly Dictionary<TKey, TCollection> original;
	private readonly Dictionary<TItem, TKey> reverse;

	/// <summary>
	///   Creates an instance of <see cref="Map{TKey,TItem,TCollection}" />.
	/// </summary>
	/// <param name="items"></param>
	/// <param name="keySelector"></param>
	/// <param name="collectionSelector"></param>
	public Map(IEnumerable<TItem> items, Func<TItem, TKey> keySelector, Func<IGrouping<TKey, TItem>, TCollection> collectionSelector)
	{
		original = items.GroupBy(keySelector).ToDictionary(group => group.Key, collectionSelector);
	}

	/// <summary>
	///   Gets original dictionary of this map.
	/// </summary>
	public IReadOnlyDictionary<TKey, TCollection> Original => original;

	/// <summary>
	///   Gets reverse dictionary of this map.
	/// </summary>
	public IReadOnlyDictionary<TItem, TKey> Reverse => reverse;

	/// <summary>
	///   Adds a pair into this one to many map.
	/// </summary>
	/// <param name="key">The key of the pair to be added.</param>
	/// <param name="item">The value of the pair to be added.</param>
	public void Add(TKey key, TItem item)
	{
		original.AddToCollection(key, item);
		reverse.Add(item, key);
	}

	/// <summary>
	///   Removes a key from this map.
	/// </summary>
	/// <param name="key">The key to be removed from this map.</param>
	/// <returns>True, if key was successfully removed, false - otherwise.</returns>
	public bool RemoveKey(TKey key)
	{
		if (!original.TryGetValue(key, out TCollection collection))
		{
			return false;
		}

		foreach (TItem item in collection)
		{
			reverse.MustRemoveKey(item);
		}

		return true;
	}

	/// <summary>
	///   Removes a key from this map.
	/// </summary>
	/// <param name="key">The key which needs to be removed.</param>
	/// <exception cref="InvalidOperationException">Is thrown, if the removal was not successful.</exception>
	public void MustRemoveKey(TKey key)
	{
		bool wasRemoved = RemoveKey(key);
		(!wasRemoved).ThrowKeyNotFoundToBeRemovedException();
	}

	/// <summary>
	///   Removes value from this map.
	/// </summary>
	/// <param name="value">The value to be removed.</param>
	/// <returns>True, if value was successfully removed. False - otherwise.</returns>
	public bool RemoveValue(TItem value)
	{
		if (!reverse.TryGetValue(value, out TKey key))
		{
			return false;
		}

		reverse.Remove(value);
		original.MustRemoveFromCollection(key, value);
		return true;
	}
}
