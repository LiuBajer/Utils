using System;

public static class ArrayExtensions
{
    public static void TransformEachElementBy<TElement>(this TElement[] array, Func<TElement, TElement> transformation)
    {
        for (var i = 0; i < array.Length; i++)
        {
            var element = array[i];
            array[i] = transformation.Invoke(element);
        }
    }

	public static int FindFirstIndexWhere<TElement>(this TElement[] array, Predicate<TElement> predicate)
	{
		var left = 0;
		var right = array.Length;
  		while (left < right)
        {
			var mid = (left + right)/2;
            if (predicate.Invoke(array[mid]))
            {
                right = mid;
            }
            else
            {
                left = mid + 1;
            }
        }
		
		return left;
    }
}
