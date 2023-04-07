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
	
    public static bool IsZero<TElement>(this TElement[][] array, (int line, int index) coordinate)
    {
        return array[coordinate.line][coordinate.index] == (dynamic) 0;
    }

    public static IEnumerable<(int line, int index)> GetAdjacents(this (int line, int index) coordinate)
    {
        yield return (coordinate.line - 1, coordinate.index);
        yield return (coordinate.line, coordinate.index - 1);
        yield return (coordinate.line, coordinate.index + 1);
        yield return (coordinate.line + 1, coordinate.index);
    }
    
    public static IEnumerable<(int line, int index)> GetAdjacentsSatisfying(this (int line, int index) coordinate, Func<(int line, int index), bool> predicate)
    {
        return coordinate.GetAdjacents().Where(predicate);
    }
}
