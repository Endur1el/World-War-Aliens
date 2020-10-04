using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct HexCoords
{
	[ReadOnly]
	public int x, y, z;
	public HexCoords(int x, int y, int z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}
	public OffsetCoords GetOffsetCoords()
	{
		return GetOffsetCoords(x,y,z);
	}

	public static OffsetCoords GetOffsetCoords(int x, int y, int z)
	{
		OffsetCoords convertedCoords = new OffsetCoords();
		convertedCoords.col = x + (int)((z - (z & 1)) / 2);
		convertedCoords.row = z;
		return convertedCoords;
	}

	public int[] GetOffsetCoordsArray()
	{
		return GetOffsetCoordsArray(x,y,z);
	}
	public static int[] GetOffsetCoordsArray(int x, int y, int z)
	{
		int[] coords = new int[2];
		coords[0] = x + (int)((z - (z & 1)) / 2);
		coords[1] = z;
		return coords;
	}
	
	public HexCoords Plus(HexCoords otherCoords)
	{
		return Plus(otherCoords.x, otherCoords.y, otherCoords.z);
	}
	public HexCoords Plus(int x, int y, int z)
	{
		int x1 = this.x + x;
		int y1 = this.y + y;
		int z1 = this.z + z;
		return new HexCoords(x1, y1, z1);
	}

	public void Add(HexCoords otherCoords)
	{
		Add(otherCoords.x, otherCoords.y, otherCoords.z);
	}
	public void Add(int x, int y, int z)
	{
		this.x += x;
		this.y += y;
		this.z += z;
	}



	public int DistanceTo(HexCoords otherCoords)
	{
		return (System.Math.Abs(x - otherCoords.x) + System.Math.Abs(y - otherCoords.y) + System.Math.Abs(z - otherCoords.z)) / 2;
	}

}
[System.Serializable]
public struct OffsetCoords
{
	[ReadOnly]
	public int col, row;

	public HexCoords GetHexCoords()
	{
		return GetHexCoords(col,row);
	}

	public static HexCoords GetHexCoords(int col, int row)
	{
		int x = col - (int)((row - (row & 1)) / 2);
		int z = row;
		int y = -x - z;

		HexCoords convertedCords = new HexCoords(x,y,z);
		return convertedCords;
	}

	public void SetCoords(int col, int row)
	{
		this.col = col;
		this.row = row;
	}
}
