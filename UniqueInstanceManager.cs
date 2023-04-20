/// <summary>
///   Responsible for containing unique instances of the same type.
/// </summary>
/// <typeparam name="TId">The type of ID of instances under this manager.</typeparam>
/// <typeparam name="TInstance">The type of instances under this manager.</typeparam>
public class UniqueInstanceManager<TId, TInstance>
{
	private readonly Dictionary<TId, TInstance> instances;

	/// <summary>
	///   Adds an instance to this manager.
	/// </summary>
	/// <param name="id">The ID of instance to be added.</param>
	/// <param name="instance">The instance to be added.</param>
	public void AddInstance(TId id, TInstance instance)
	{
		instances.Add(id, instance);
	}

	/// <summary>
	///   Gets an instance with a certain ID which is under this manager control.
	/// </summary>
	/// <param name="id">The ID of an instance that needs to be accessed.</param>
	/// <returns>An instance of no instance in case, there is no instance with such ID created.</returns>
	public Maybe<TInstance> GetInstance(TId id)
	{
		bool wasFound = instances.TryGetValue(id, out TInstance instance);
		var maybe = new Maybe<TInstance>();
		if (wasFound)
		{
			maybe.Add(instance);
		}
		return maybe;
	}

	/// <summary>
	///   Gets an instance with a certain ID which is under this manager control.
	///   If there is no instance with this ID added to the manager, throws a standard key not found exception.
	/// </summary>
	/// <param name="id">The ID of an instance that needs to be accessed.</param>
	/// <returns>The instance with the specified ID.</returns>
	public TInstance MustGet(TId id)
	{
		return instances[id];
	}

	/// <summary>
	///   Gets whether an instance of a certain ID is under the control of this manager.
	/// </summary>
	/// <param name="id">The ID to be checked.</param>
	/// <returns>True if this manager contains the given ID, false - otherwise.</returns>
	public bool HasInstance(TId id)
	{
		return instances.ContainsKey(id);
	}
}
