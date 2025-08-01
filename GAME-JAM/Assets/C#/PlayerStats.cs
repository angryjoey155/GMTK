using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    public static int PlayerMaxAmmo = 1;
    static int PlayerAmmo = PlayerMaxAmmo;
    public static float PlayerMaxHealth = 1;
    static float PlayerHealth = PlayerMaxHealth;
    public static void ChangeAmmo(int amount)
    {
        if (PlayerAmmo + amount >= 0) PlayerAmmo += amount;                             //if player under or = to 0 then don't change
        if (PlayerMaxAmmo < PlayerAmmo + amount) PlayerAmmo = PlayerMaxAmmo;            //if overflow then set ammo to max
    }

    public static void ChangeHealth(int amount)
    {
        if (PlayerHealth + amount >= 0) PlayerHealth += amount;                         //if player under or = to 0 then don't change
        if (PlayerMaxHealth < PlayerHealth + amount) PlayerHealth = PlayerMaxHealth;    //if overflow then set health to max
    }
    public static int GetPlayerAmmo()
    {
        return PlayerAmmo;
    }
    public static float GetPlayerHealth()
    {
        return PlayerHealth;
    }
}
