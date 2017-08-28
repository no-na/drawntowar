﻿using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{

    public int health;
    public SoldierController myController;


    // Update is called once per frame
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            WinLoss loss = new WinLoss();
            loss.Lose();
            print("Castle is dead");
            //myController.Die();
        }
    }
}
