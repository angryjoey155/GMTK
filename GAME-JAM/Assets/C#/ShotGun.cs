using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : MonoBehaviour
{
    [SerializeField] GameObject AimRadiusPrefab;
    [SerializeField] Transform ShotGunLoc;
    [SerializeField] int RecoilPower = 300;

    GameObject AimRadius;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))//aim in
        {
            AimState();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))//aim out and shot
        {
            Shoot();
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))//aim out and shot
        {

        }
    }

    private void Shoot()
    {
        List<GameObject> temp = AimRadius.GetComponent<Killzone>().getAllEnemies();
        int totalEnemies = temp.Count;

        if (temp != null)
        {
            for (int i = totalEnemies - 1; i >= 0; i--)
            {
                Destroy(temp[i]);
            }
        }

        Destroy(AimRadius);
        Recoil(totalEnemies);
    }
    private void Recoil(int amountOfRecoil)
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z; // Fix z-depth
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 direction = mousePosition - transform.position;
        direction.Normalize();


        amountOfRecoil++;
        Vector2 temp;
        temp.x = direction.x * amountOfRecoil * RecoilPower;
        temp.y = direction.y * amountOfRecoil * RecoilPower;

        GetComponent<Movement>().AddForce(-temp);
        Debug.Log(temp);
    }


    private void AimState()
    {
            AimRadius = Instantiate(AimRadiusPrefab, ShotGunLoc);
    }
}