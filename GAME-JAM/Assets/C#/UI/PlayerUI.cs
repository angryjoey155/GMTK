using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] GameObject[] health;
    [SerializeField] GameObject[] ammo;
    [SerializeField] Resourses[] healthResource;
    [SerializeField] Resourses[] ammoResource;
//zero scalability i hate this so much
//
//its ok this is like our first finished game))
//Don't beat yourself up too much) loves yous <3
    private void Update()
    {
        HealthChange((int)PlayerStats.GetPlayerHealth(), health);
        AmmoChange(PlayerStats.GetPlayerAmmo(), ammo);
    }
    

    void HealthChange(int i, GameObject[] objArray)
    {
        for (int j = 0; j < objArray.Length; j++)
        {
            if (j < i)
                healthResource[j].ChangeAnim("FullHeart");
            else
                healthResource[j].ChangeAnim("BrokenHeart");
        }
    }
    void AmmoChange(int i, GameObject[] objArray)
    {
        for (int j = 0; j < objArray.Length; j++)
        {
            if (j < i)
                ammoResource[j].ChangeAnim("FullAmmo");
            else
                ammoResource[j].ChangeAnim("BrokenAmmo");
        }
    }
}
