public static class PlayerStats
{
    public static int PlayerMaxAmmo = 3;
    static int PlayerAmmo = PlayerMaxAmmo;
    public static float PlayerMaxHealth = 3;
    static float PlayerHealth = PlayerMaxHealth;
    static bool isDead;

    public static void ChangeAmmo(int amount)
    {
        if (PlayerAmmo + amount >= 0) PlayerAmmo += amount;                             //if player under or = to 0 then don't change
        if (PlayerMaxAmmo < PlayerAmmo + amount) PlayerAmmo = PlayerMaxAmmo;            //if overflow then set ammo to max
    }

    public static void ChangeHealth(int amount)
    {
        if (PlayerHealth + amount >= 0) PlayerHealth += amount;                         //if player under or = to 0 then don't change
        if (PlayerMaxHealth < PlayerHealth + amount) PlayerHealth = PlayerMaxHealth;    //if overflow then set health to max
        if (PlayerHealth <= 0) isDead = true;
        else isDead = false;
    }
    public static bool GetIsDead()
    {
        return isDead; 
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
