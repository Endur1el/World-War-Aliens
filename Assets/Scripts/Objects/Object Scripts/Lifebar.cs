using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifebar : MonoBehaviour
{
    public Pawn myPawn;
    public GameObject[] lifeBoxes;
    public GameObject text;

    public void InitLife(Pawn myPawn)
    {
        this.myPawn = myPawn;
        transform.parent = myPawn.gameObject.transform;
        DisplayLife();
    }

    public void DisplayLife()
    {
        foreach (GameObject g in this.lifeBoxes)
            g.SetActive(true);
        text.GetComponent<TextMesh>().text = myPawn.name + "\n" + myPawn.stat.HP + " / " + myPawn.stat.maxHP;
    }

    public void UpdateLife()
    {
        transform.parent = myPawn.gameObject.transform;
        text.GetComponent<TextMesh>().text = myPawn.name + "\n" + myPawn.stat.HP + " / " + myPawn.stat.maxHP;
        transform.position = myPawn.gameObject.transform.position + new Vector3(0, 1.25f, -1.35f);
        float test = 10 * ((float)myPawn.stat.HP / (float)myPawn.stat.maxHP);
        int remainingTens = (int)(test);
        for (int i = 9; i > remainingTens; i--)
        {
            this.lifeBoxes[i].SetActive(false);
        }
    }

}
