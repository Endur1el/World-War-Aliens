using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameObject pawnPrefab;
    public GameObject hexGrid;
    public GameObject hexPrefab;
    public GameObject LifebarPrefab;
	public GameObject testSphere;

	public HexMaterials hexMaterials;

	private bool started = false;

    public List<GameObject> nazis = new List<GameObject>();
    public List<GameObject> aliens = new List<GameObject>();


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
        Pawn.LifebarPrefab = LifebarPrefab;
		DontDestroyOnLoad(gameObject);
        Test();
        hexGrid.GetComponent<HexGridNew>().MakeBoard(20);
		hexGrid.GetComponent<HexGridNew>().SpawnPawns(aliens);
		hexGrid.GetComponent<HexGridNew>().SpawnPawns(nazis);
    }
	private void Update()
	{
	}

	public void Test()
    {
		GameObject objectAlien1 = Instantiate(pawnPrefab);
		GameObject objectAlien2 = Instantiate(pawnPrefab);
		GameObject objectNazi1 = Instantiate(pawnPrefab);

		objectAlien1.GetComponent<Pawn>().Init("Alien Test 1", Side.alien, 10, 4, 3, 50, 6, 4, 60, 0, 10, 90);
		objectAlien2.GetComponent<Pawn>().Init("Alien Test 2", Side.alien, 10, 4, 3, 50, 6, 4, 60, 0, 10, 90);
		objectNazi1.GetComponent<Pawn>().Init("Nazi Test 1", Side.nazi, 10, 4, 9, 50, 6, 4, 60, 0, 10, 90);
        aliens.Add(objectAlien1);
        aliens.Add(objectAlien2);
        nazis.Add(objectNazi1);
    }

	public List<GameObject> GetOppositeSidePawns(Side mySide)
	{
		if (Side.nazi.Equals(mySide))
		{
			return aliens;
		} else
		{
			return nazis;
		}
	}

	public List<GameObject> GetMySidePawns(Side mySide)
	{
		if (Side.nazi.Equals(mySide))
		{
			return nazis;
		}
		else
		{
			return aliens;
		}
	}

	public List<GameObject> GetAllPawns()
	{
		List<GameObject> pawns = new List<GameObject>();
		pawns.AddRange(nazis);
		pawns.AddRange(aliens);
		return pawns;
	}

}

