using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class HexGridNew : MonoBehaviour
{
    public int size;

	public GameObject hexPrefab;

    public GameObject[,] board;
	public HashSet<Hex> hexes = new HashSet<Hex>();

    private void GenerateMap()
    {
        for (int col = 0; col < size; col++)
        {
            Vector3 pos = gameObject.transform.position;
            if (col % 2 != 0)
                pos.z -= 1.5f;
            pos.x = gameObject.transform.position.x + 2.25f * col;

			for (int row = 0; row < size; row++)
			{
				GameObject hexObject = Instantiate(hexPrefab);
				Hex hex = hexObject.GetComponent<Hex>();
				hexes.Add(hex);
				
				pos.z -= 3;
				hexObject.transform.position = pos;
				hexObject.transform.parent = transform;
				hexObject.name = row + ", " + col;

				board[row, col] = hexObject;
				hex.offsetCoords.SetCoords(row, col);
				hex.hexCoords = hex.offsetCoords.GetHexCoords();
			}
        }
    }

	public HashSet<Hex> GetHexesWithinRange(Hex hex, int range)
	{
		HashSet<Hex> possibleHexes = new HashSet<Hex>();
		for (int x = -range; x <= range; x++)
		{
			for (int y = System.Math.Max(-x-range,-range); y <= System.Math.Min(-x + range, range); y++)
			{
				int z = -x - y;
				HexCoords tempHexCoords = new HexCoords(x, y, z);
				tempHexCoords.Add(hex.hexCoords);
				if (HexAtExists(tempHexCoords)) possibleHexes.Add(GetHexAtCoords(tempHexCoords).GetComponent<Hex>());
			}
		}
		return possibleHexes;
	}

	public GameObject GetHexAtCoords(HexCoords coords)
	{
		int[] coordsArray = coords.GetOffsetCoordsArray();
		if (HexAtExists(coordsArray[0],coordsArray[1]))return board[coordsArray[0], coordsArray[1]];
		return null;
	}

	public bool HexAtExists(int col, int row)
	{
		if (col >= 0 && (col <= size - 1) && row >= 0 && (row <= size - 1)) return true;
		return false;
	}

	public bool HexAtExists(HexCoords coords)
	{
		int[] coordsArray = coords.GetOffsetCoordsArray();
		int col = coordsArray[0];
		int row = coordsArray[1];
		return HexAtExists(col, row);
	}

	public GameObject GetHexAtCoords(int x, int y, int z)
	{
		int[] coordsArray = HexCoords.GetOffsetCoordsArray(x,y,z);
		return board[coordsArray[0], coordsArray[1]];
	}

	public void SpawnPawns(List<GameObject> pawns)
	{
		int nazix = 0, naziy = 0, alienx = 0, alieny = 0;
		foreach (GameObject pawnObject in pawns)
		{
			Pawn pawn = pawnObject.GetComponent<Pawn>();

			if (pawn.side.Equals(Side.nazi))
			{
				pawn.SetHex(board[nazix, naziy].GetComponent<Hex>());
				pawn.UpdatePosition();
				nazix++;
			}
			else
			{
				alieny = board.GetLength(1)-1;
				pawn.SetHex(board[alienx, alieny].GetComponent<Hex>());
				pawn.UpdatePosition();
				alienx++;
			}
		}

	}

    public void MakeBoard(int size)
    {
        this.size = size;
        board = new GameObject[size, size];
        GenerateMap();
    }



	public void ClearBoard()
	{
		foreach(Hex hex in hexes)
		{
			hex.isPossibleMoveTarget = false;
			hex.SetMaterial(GameManager.instance.hexMaterials.normal);
		}
		foreach(GameObject pawnObject in GameManager.instance.GetAllPawns())
		{
			pawnObject.GetComponent<Pawn>().logic.isShootable = false;
		}
	}

}
