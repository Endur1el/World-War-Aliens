using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameHandler
{
	public static void DeselectAllPawns()
	{
		foreach(GameObject pawnObject in GameManager.instance.GetAllPawns())
		{
			pawnObject.GetComponent<Pawn>().logic.isSelected = false;
			pawnObject.GetComponent<Pawn>().logic.isShootable = false;
		}
	}

	public static void ClearBoard()
	{
		GameManager.instance.hexGrid.GetComponent<HexGridNew>().ClearBoard();
	}

	public static Pawn FindSelectedPawn()
	{
		Pawn selectedPawn = null;
		foreach(GameObject pawnObject in GameManager.instance.GetAllPawns())
		{
			if (pawnObject.GetComponent<Pawn>().logic.isSelected)
			{
				selectedPawn = pawnObject.GetComponent<Pawn>();
			}
		}
		return selectedPawn;
	}
	

}
