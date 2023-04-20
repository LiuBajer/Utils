/// <summary>
///   Responsible for instantiation of unique instances.
/// </summary>
/// <typeparam name="TId">Type of ID which distinguishes separate instances.</typeparam>
/// <typeparam name="TInstance">Type of instances which are controlled by this manager.</typeparam>
public class UniqueInstantiationManager<TId, TInstance> : UniqueInstanceManager<TId, TInstance>
{
	private readonly Func<TId, TInstance> constructor;

	/// <summary>
	///   Creates an instance of <see cref="UniqueInstantiationManager{TId,TInstance}" />.
	/// </summary>
	/// <param name="constructor">The constructor of instance type.</param>
	public UniqueInstantiationManager(Func<TId, TInstance> constructor)
	{
		this.constructor = id =>
		{
			TInstance instance = constructor.Invoke(id);
			AddInstance(id, instance);
			return instance;
		};
	}

	/// <summary>
	///   Gets an instance with a given ID, if one does not exist, creates it.
	/// </summary>
	/// <param name="id">The ID of an instance which has to be accessed.</param>
	/// <returns>An instance containing the given ID.</returns>
	public TInstance GetOrCreate(TId id)
	{
		Maybe<TInstance> maybeInstance = GetInstance(id);
		if (maybeInstance.Any())
		{
			return maybeInstance.Single();
		}

		TInstance newInstance = constructor.Invoke(id);
		return newInstance;
	}
}

/// <summary>
///   Responsible for instantiation of unique instances.
/// </summary>
/// <typeparam name="TId">Type of ID which distinguishes separate instances.</typeparam>
/// <typeparam name="TInstance">Type of instances which are controlled by this manager.</typeparam>
public class UniqueInstantiationManager<TId, TInstance> : UniqueInstanceManager<TId, TInstance>
{
	private readonly Func<TId, TInstance> constructor;

	/// <summary>
	///   Creates an instance of <see cref="UniqueInstantiationManager{TId,TInstance}" />.
	/// </summary>
	/// <param name="constructor">The constructor of instance type.</param>
	public UniqueInstantiationManager(Func<TId, TInstance> constructor)
	{
		this.constructor = id =>
		{
			TInstance instance = constructor.Invoke(id);
			AddInstance(id, instance);
			return instance;
		};
	}

	/// <summary>
	///   Gets an instance with a given ID, if one does not exist, creates it.
	/// </summary>
	/// <param name="id">The ID of an instance which has to be accessed.</param>
	/// <returns>An instance containing the given ID.</returns>
	public TInstance GetOrCreate(TId id)
	{
		Maybe<TInstance> maybeInstance = GetInstance(id);
		if (maybeInstance.Any())
		{
			return maybeInstance.Single();
		}

		TInstance newInstance = constructor.Invoke(id);
		return newInstance;
	}
}
