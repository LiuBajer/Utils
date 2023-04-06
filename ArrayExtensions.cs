using System;

public static class ArrayExtensions
{
	public static int FindFirstIndexWhere<TItem>(this TItem[] array, Predicate<TItem> predicate)
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
