using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Hex : MonoBehaviour
{

    public HashSet<Hex> neighbors = new HashSet<Hex>();

	public GameObject temp;
	public Example radiusFinder;
	public bool isPossibleMoveTarget;

	public HexCoords hexCoords;
	public OffsetCoords offsetCoords;

	public void OnMouseOver()
	{
		if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1) && !Input.GetMouseButtonDown(3)) return;
		if (isPossibleMoveTarget)
		{
			GameHandler.FindSelectedPawn().MoveTo(this);
			GameHandler.ClearBoard();
		}
	}

	public void FindNeighbors()
	{
		neighbors = new HashSet<Hex>();
		Collider[] colliderNeighbors = Physics.OverlapSphere(gameObject.transform.position, 2);
		foreach (Collider collider in colliderNeighbors)
		{
			Hex tempHex = collider.gameObject.GetComponent<Hex>();
			if (((UnityEngine.Object)tempHex != null) && (collider.gameObject != gameObject))
			{
				neighbors.Add(collider.gameObject.GetComponent<Hex>());
			}
		}
	}

	public HashSet<Hex> GetHexesWithin(int range)
	{
		if (range == 0) return new HashSet<Hex>();
		HashSet<Hex> hexesWithinRange = new HashSet<Hex>();
		hexesWithinRange.UnionWith(neighbors);
		foreach (Hex neighbor in neighbors)
		{
			hexesWithinRange.UnionWith(neighbor.GetHexesWithin(range - 1));
		}
		return hexesWithinRange;
	}


	public void SetMaterial(Material m)
    {
        gameObject.GetComponent<MeshRenderer>().material = m;
    }
}
