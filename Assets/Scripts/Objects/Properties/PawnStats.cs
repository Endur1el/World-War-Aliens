using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Side
{
    alien,
    nazi
}

namespace PawnStats
{
    [System.Serializable]
    public struct GameStats
    {
        public int cost, initiative, movement, maxHP, HP, DMG, range, absrange, dodge, accuracy;
		public double armor, armorPierce;
	}
	[System.Serializable]
	public struct GameLogic
	{
		public bool hasMoved;
		public bool hasShot;
		public bool isSelected;
		public bool isShootable;
		public bool isMovable;
	}
}