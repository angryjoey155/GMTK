using System.Collections.Generic;
using UnityEngine;

public class ShotGun : MonoBehaviour
{
    [SerializeField] GameObject AimRadiusPrefab;
    [SerializeField] Transform ShotGunLoc;
    [SerializeField] int RecoilPower = 300;
    private int _ammoConsumption = -1;
    private static bool _isReloading = false;
    [SerializeField] Cooldown _reloadTime;
    [SerializeField] private AudioClip _shootAC;
    [SerializeField] private AudioClip _ReloadAC;
    GameObject AimRadius;
    bool isAimIn;

    [SerializeField] GameObject _reloadBar;
    
    void Update()
    {
        if (PlayerStats.GetIsDead()) 
        {
            _isReloading = false ;
            _reloadBar.SetActive(false);
            return; 
        }

        if (_isReloading && !_reloadTime.IsCoolingDown && PlayerStats.GetPlayerAmmo() < PlayerStats.PlayerMaxAmmo)
        {
            EndReload();
        }
        if (_isReloading)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0))   //aim in
        {
            //try
            //{
            //        Destroy(AimRadius);
            //}
            //catch { }
            if (PlayerStats.GetPlayerAmmo() > 0)
            {
                AimState();
                isAimIn = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && isAimIn)     //aim out and shot
        {
            if (!isShotCancelled)
            {
                EndOfShot();
                isAimIn = false;
            }
            else
            {
                isShotCancelled = false;
            }
        }
        
       
        if (Input.GetKeyDown(KeyCode.R) && !_isReloading && PlayerStats.GetPlayerAmmo() < PlayerStats.PlayerMaxAmmo) //Reload
        {
            AudioSource.PlayClipAtPoint(_ReloadAC, transform.position);
            _isReloading = true;
            _reloadBar.SetActive(true);
            _reloadTime.StartCooldown();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && isAimIn)
        {
            isShotCancelled = true;
            isAimIn = false;
        }
        if (!isAimIn)
        {
            Destroy(AimRadius);

        }
    }

    bool isShotCancelled;
    private void EndOfShot()
    {
        if (PlayerStats.GetPlayerAmmo() > 0)
        {
            AudioSource.PlayClipAtPoint(_shootAC, transform.position);

            Shoot();
        }
        else
        {
            //TODO
        }
    }

    void EndReload()
    {
        _isReloading = false;
        PlayerStats.ChangeAmmo(PlayerStats.PlayerMaxAmmo);
        _reloadBar.SetActive(false);

    }
    private void Shoot()
    {
        PlayerStats.ChangeAmmo(_ammoConsumption);
        List<GameObject> temp = null;
        int totalEnemies = 0    ;
        try
        {
            temp = AimRadius.GetComponent<Killzone>().getAllEnemies();
        
        totalEnemies = temp.Count;

        if (temp != null)
        {
            for (int i = totalEnemies - 1; i >= 0; i--)
            {
                Destroy(temp[i]);
            }
        }

        Recoil(totalEnemies);
        }
        catch { }
        Destroy(AimRadius);
        if (totalEnemies >= 1)
            PlayerStats.ChangeAmmo(1);
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
    }


    private void AimState()
    {
            AimRadius = Instantiate(AimRadiusPrefab, ShotGunLoc);
    }
    public static bool GetIsReloading()
    {
        return _isReloading;
    }
}