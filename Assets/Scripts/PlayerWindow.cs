using UnityEngine;
using System.Collections.Generic;
using Fps;
using System.Linq;

public class GUIUtils
{
	public static void PlayersWindow (List<Player> players, Rect rect)
	{
		if (players.Count == 0)
			return;

		string content = players
			.Select (it => it.ToString ())
			.OrderBy (desc => desc)
			.Aggregate ((nameA, nameB) => nameA + "\n" + nameB);
		Util.GUI.TextWindow ("Players", content, rect);
	}
}

