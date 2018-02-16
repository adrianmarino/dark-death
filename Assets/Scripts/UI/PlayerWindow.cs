using UnityEngine;
using System.Collections.Generic;
using Fps.Player;
using System.Linq;

namespace Fps
{
	public class GuiUtils
	{
		public static void PlayersWindow (List<PlayerState> players, Rect rect)
		{
			if (players.Count == 0) return;

			var content = players.Select (it => it.ToString ())
				.OrderBy (desc => desc)
				.Aggregate ((nameA, nameB) => nameA + "\n" + nameB);

			Util.GUI.TextWindow ("Players", content, rect);
		}
	}
}

