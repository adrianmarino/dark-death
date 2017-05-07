using System.Collections.Generic;

namespace Util
{
	public static class CollectionExtensions
	{
		public static T Next<T> (this IList<T> list, T item)
		{
			return list [NextPosition (list, item)];
		}

		public static T Previous<T> (this IList<T> list, T item)
		{
			return list [PreviousPosition (list, item)];
		}

		public static int NextPosition<T> (this IList<T> list, T item)
		{
			return (list.IndexOf (item) + 1) == list.Count ? 0 : (list.IndexOf (item) + 1);
		}

		public static int PreviousPosition<T> (this IList<T> list, T item)
		{
			return (list.IndexOf (item) - 1) < 0 ? list.Count - 1 : (list.IndexOf (item) - 1);
		}
	}
}
