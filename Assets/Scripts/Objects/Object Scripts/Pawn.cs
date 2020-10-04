using PawnStats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
	public Hex hex;
	public Side side;
	public bool alive;

	public GameStats stat;
	public GameLogic logic;

	public GameObject lifebar;
	public static GameObject LifebarPrefab;

	public void Start()
	{
		logic.hasMoved = false;
		logic.hasShot = false;
		logic.isMovable = true;
		logic.isSelected = false;
		logic.isShootable = false;
	}
	public void Init(string name, Side side, int cost, int initiative, int movement, int maxHP, int DMG, int range, double armorPierce, double armor, int dodge, int accuracy)
    {
        this.name = name;

        this.side = side;

        alive = true;

        stat.cost = cost;
        stat.initiative = initiative;
        stat.movement = movement;
        stat.maxHP = maxHP;
        stat.HP = maxHP;
        stat.DMG = DMG;
        stat.range = range;
        stat.armorPierce = armorPierce;
        stat.armor = armor;
        stat.dodge = dodge;
        stat.accuracy = accuracy;

		lifebar = MonoBehaviour.Instantiate(LifebarPrefab);
		lifebar.GetComponent<Lifebar>().InitLife(this);
		lifebar.GetComponent<Lifebar>().UpdateLife();
	}

	public void UpdatePosition()
	{
		gameObject.transform.parent = hex.transform;
		gameObject.transform.position = hex.transform.position;
		gameObject.transform.Translate(0, .5f, 0);
	}

	public void MoveTo(Hex targetHex)
	{
		hex = targetHex;
		UpdatePosition();
	}

    public void SetHex(Hex hex)
    {
        this.hex = hex;
    }

	public void ShootAt(Pawn targetPawn)
	{
		logic.isSelected = false;
		if (!ShotHit(targetPawn)) return;
		targetPawn.GetHit(ResultDamage(targetPawn));
		print(ResultDamage(targetPawn));
	}

	private int ResultDamage(Pawn targetPawn)
	{
		if (targetPawn.stat.armor - stat.armorPierce < 0)
		{
			return stat.DMG;
		} else
		{
			return (int)Math.Ceiling((double)stat.DMG * (1d + (targetPawn.stat.armor - stat.armorPierce) / 100d));

		}
	}
	private bool ShotHit(Pawn targetPawn)
	{ 
		if (UnityEngine.Random.Range(1, 100) < stat.accuracy - targetPawn.stat.dodge) return true;
		return false;
	}

    public void GetHit(int damage)
    {
        if (stat.HP - damage <= 0)
        {
            damage = stat.HP;
            TakeDamage(damage);
            Die();
        }
        else
        {
            TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage)
    {
        stat.HP -= damage;
		lifebar.GetComponent<Lifebar>().UpdateLife();
		//Visual code for taking damage
	}

	public void Die()
    {
    }

    public void GetHealed(int heal)
    {
        if (heal + stat.HP > stat.maxHP)
        {
            heal = stat.maxHP - stat.HP;
            Heal(heal);
        }
        else
        {
            Heal(heal);
        }
    }

    public void Heal(int heal)
    {
        stat.HP += heal;
        lifebar.GetComponent<Lifebar>().UpdateLife();
        //Visual code for getting healed
    }

	public void OnMouseOver()
	{
		if(!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1) && !Input.GetMouseButtonDown(2)) return;
		if (Input.GetMouseButtonDown(2))
		{
			GameManager.instance.hexGrid.GetComponent<HexGridNew>().ClearBoard();
			print("Cleared");
			return;
		}
		if (logic.isShootable)
		{
			GameHandler.FindSelectedPawn().ShootAt(this);
			return;
		}
		GameHandler.DeselectAllPawns();
		logic.isSelected = true;
		if (Input.GetMouseButtonDown(0))
		{
			print("Attack");
			GameManager.instance.hexGrid.GetComponent<HexGridNew>().ClearBoard();
			AttackHighlight();
		} else if (Input.GetMouseButtonDown(1))
		{
			print("Move");
			GameManager.instance.hexGrid.GetComponent<HexGridNew>().ClearBoard();
			MoveHighlight();
		}
	}

	public void AttackHighlight()
	{
		
		HashSet<Hex> hexesWithinRange = GameManager.instance.hexGrid.GetComponent<HexGridNew>().GetHexesWithinRange(hex, stat.range);

		foreach(Hex currentHex in hexesWithinRange)
		{
			currentHex.SetMaterial(GameManager.instance.hexMaterials.yellow);
		}

		foreach (GameObject pawnObject in GameManager.instance.GetOppositeSidePawns(side))
		{
			if (hexesWithinRange.Contains(pawnObject.GetComponent<Pawn>().hex))
			{
				pawnObject.GetComponent<Pawn>().hex.SetMaterial(GameManager.instance.hexMaterials.red);
				pawnObject.GetComponent<Pawn>().logic.isShootable = true;
			}
		}

		foreach (GameObject pawnObject in GameManager.instance.GetMySidePawns(side))
		{
			if (hexesWithinRange.Contains(pawnObject.GetComponent<Pawn>().hex))
			{
				pawnObject.GetComponent<Pawn>().hex.SetMaterial(GameManager.instance.hexMaterials.blue);
			}
		}
		hex.SetMaterial(GameManager.instance.hexMaterials.normal);
	}

	public void MoveHighlight()
	{
		HashSet<Hex> hexesWithinRange = GameManager.instance.hexGrid.GetComponent<HexGridNew>().GetHexesWithinRange(hex, stat.movement);

		foreach (Hex currentHex in hexesWithinRange)
		{
			currentHex.SetMaterial(GameManager.instance.hexMaterials.green);
			currentHex.isPossibleMoveTarget = true;
		}

		foreach (GameObject pawnObject in GameManager.instance.GetOppositeSidePawns(side))
		{
			if (hexesWithinRange.Contains(pawnObject.GetComponent<Pawn>().hex))
			{
				pawnObject.GetComponent<Pawn>().hex.SetMaterial(GameManager.instance.hexMaterials.normal);
				pawnObject.GetComponent<Pawn>().hex.isPossibleMoveTarget = false;
			}
		}

		foreach (GameObject pawnObject in GameManager.instance.GetMySidePawns(side))
		{
			if (hexesWithinRange.Contains(pawnObject.GetComponent<Pawn>().hex))
			{
				pawnObject.GetComponent<Pawn>().hex.SetMaterial(GameManager.instance.hexMaterials.normal);
				pawnObject.GetComponent<Pawn>().hex.isPossibleMoveTarget = false;
			}
		}
	}
}
