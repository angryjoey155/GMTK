using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShotGun : MonoBehaviour
{
    [SerializeField] GameObject AimRadiusPrefab;
    [SerializeField] Transform ShotGunLoc;
    [SerializeField] int RecoilPower = 300;
    private int _ammoConsumption = -1;
    private bool _isReloading = false;
    [SerializeField] Cooldown _reloadTime;
    [SerializeField] private AudioClip _shootAC;
    [SerializeField] private AudioClip _reloadAC;
    GameObject AimRadius;

    [SerializeField] GameObject _reloadBar;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))   //aim in
        {
            if (PlayerStats.GetPlayerAmmo() > 0 && !_isReloading)
            {
                AimState();
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))     //aim out and shot
        {
            if (!isShotCancelled)
            {
                EndOfShot();
            }
            else
            {
                isShotCancelled = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && !_isReloading && PlayerStats.GetPlayerAmmo() < PlayerStats.PlayerMaxAmmo) //Reload
        {
            AudioSource.PlayClipAtPoint(_reloadAC, transform.position);
            _isReloading = true;
            _reloadBar.SetActive(true);
            _reloadTime.StartCooldown();
        }
        bool huh = PlayerStats.GetPlayerAmmo() <= 0;
        if (_isReloading && !_reloadTime.IsCoolingDown && PlayerStats.GetPlayerAmmo() < PlayerStats.PlayerMaxAmmo) 
        {
                EndReload();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Destroy(AimRadius);
            isShotCancelled = true;
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
        _isReloading = false ;
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

        Destroy(AimRadius);
        Recoil(totalEnemies);
        }
        catch { }
        if(totalEnemies >= 1)
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
}