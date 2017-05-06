using System.Collections.Generic;

namespace Util
{
	public static class CollectionExtensions
	{
		public static T Next<T> (this IList<T> list, T item)
		{
			int index = (list.IndexOf (item) + 1) == list.Count ? 0 : (list.IndexOf (item) + 1);
			UnityEngine.Debug.Log ("index: " + index);
			return list [index];
		}

		public static T Previous<T> (this IList<T> list, T item)
		{
			int index = (list.IndexOf (item) - 1) < 0 ? list.Count - 1 : (list.IndexOf (item) - 1);
			UnityEngine.Debug.Log ("index: " + index);
			return list [index];
		}
	}
}
