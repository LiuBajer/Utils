/// <summary>
///   Contains extension methods for <see cref="IDictionary{TKey,TValue}" />.
/// </summary>
public static class DictionaryExtensions
{
	/// <summary>
	///   Removes a key from dictionary. Throws an exception if the removal is not successful.
	/// </summary>
	/// <typeparam name="TKey">The type of key of dictionary.</typeparam>
	/// <typeparam name="TValue">The type of value of dictionary.</typeparam>
	/// <param name="dictionary">The dictionary from which a key needs to be removed.</param>
	/// <param name="key">The key, which has to be removed from dictionary.</param>
	public static void MustRemoveKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
	{
		bool wasRemoved = dictionary.Remove(key);
		(!wasRemoved).ThrowKeyNotFoundToBeRemovedException();
	}

	/// <summary>
	///   Adds an item into a collection, which a value of a dictionary.
	/// </summary>
	/// <typeparam name="TKey">The type of key of the dictionary.</typeparam>
	/// <typeparam name="TItem">The type of items in collection which is a value of the dictionary.</typeparam>
	/// <typeparam name="TCollection">The type of value of the dictionary, which must be a <see cref="ICollection{TItem}" />.</typeparam>
	/// <param name="dictionary">The dictionary, into which the item has to be added.</param>
	/// <param name="key">The key at which the item needs to be added.</param>
	/// <param name="item">The item which has to be added into dictionary value.</param>
	public static void AddToCollection<TKey, TItem, TCollection>(this IDictionary<TKey, TCollection> dictionary, TKey key, TItem item)
		where TCollection : ICollection<TItem>, new()
	{
		bool isKeyPresent = dictionary.TryGetValue(key, out TCollection collection);
		if (!isKeyPresent)
		{
			collection = new TCollection();
			dictionary[key] = collection;
		}

		collection.Add(item);
	}

	/// <summary>
	///   Removes an item from a collection, which is value of a dictionary.
	///   If the collection becomes empty, the key is also removed.
	///   If the removal of item is not successful, an exception is thrown.
	/// </summary>
	/// <typeparam name="TKey">The type of key of the dictionary.</typeparam>
	/// <typeparam name="TItem">The type of items in collection which is a value of the dictionary.</typeparam>
	/// <typeparam name="TCollection">The type of value of the dictionary, which must be a <see cref="ICollection{TItem}" />.</typeparam>
	/// <param name="dictionary">The dictionary, from which the item has to be removed.</param>
	/// <param name="key">The key at which the item needs to be removed.</param>
	/// <param name="item">The item which has to be removed from dictionary.</param>
	public static void MustRemoveFromCollection<TKey, TItem, TCollection>(this IDictionary<TKey, TCollection> dictionary, TKey key, TItem item)
		where TCollection : ICollection<TItem>
	{
		bool wasRemoved = dictionary.RemoveFromCollection(key, item);
		(!wasRemoved).ThrowKeyNotFoundToBeRemovedException();
	}

	/// <summary>
	///   Removes an item from a collection, which is value of a dictionary.
	///   If the collection becomes empty, the key is also removed.
	/// </summary>
	/// <typeparam name="TKey">The type of key of the dictionary.</typeparam>
	/// <typeparam name="TItem">The type of items in collection which is a value of the dictionary.</typeparam>
	/// <typeparam name="TCollection">The type of value of the dictionary, which must be a <see cref="ICollection{TItem}" />.</typeparam>
	/// <param name="dictionary">The dictionary, from which the item has to be removed.</param>
	/// <param name="key">The key at which the item needs to be removed.</param>
	/// <param name="item">The item which has to be removed from dictionary.</param>
	/// <returns>True, if item was successfully removed, false - otherwise.</returns>
	public static bool RemoveFromCollection<TKey, TItem, TCollection>(this IDictionary<TKey, TCollection> dictionary, TKey key, TItem item)
		where TCollection : ICollection<TItem>
	{
		if (!dictionary.TryGetValue(key, out TCollection collection))
		{
			return false;
		}

		bool wasRemoved = collection.Remove(item);
		if (collection.Count <= 0)
		{
			dictionary.MustRemoveKey(key);
		}
		return wasRemoved;
	}
}
