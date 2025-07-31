using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    static int PlayerAmmo;
    public static int PlayerMaxAmmo;
    static float PlayerHealth;
    public static float PlayerMaxHealth;
    static void ChangeAmmo(int amount)
    {
        if (PlayerAmmo + amount >= 0) PlayerAmmo = amount;                      //if player under or = to 0 then don't change
        if (PlayerMaxAmmo < PlayerAmmo + amount) PlayerAmmo = PlayerMaxAmmo;    //if overflow then set ammo to max
    }

    static void ChangeHealth(int amount)
    {
        
    }
}
