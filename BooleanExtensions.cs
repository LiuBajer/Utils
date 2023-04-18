/// <summary>
///   Contains extension methods for <see cref="bool" />.
/// </summary>
public static class BooleanExtensions
{
	/// <summary>
	///   Throws a "Key was not found to be removed" exception, if requested.
	/// </summary>
	/// <param name="throwException">True, if this method needs to throw exception, false - otherwise.</param>
	/// <exception cref="InvalidOperationException">Is thrown if boolean parameter is true.</exception>
	public static void ThrowKeyNotFoundToBeRemovedException(this bool throwException)
	{
		if (throwException)
		{
			throw new InvalidOperationException("Key was not found to be removed.");
		}
	}
}
