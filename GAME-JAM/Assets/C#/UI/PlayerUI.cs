using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Image[] healthArray;
    [SerializeField] Image[] ammoArray;
    [SerializeField] Sprite brokenSprite;
    [SerializeField] Sprite fullSprite;


    private void Update()
    {
        StatChange((int)PlayerStats.GetPlayerHealth(), healthArray);
        StatChange(PlayerStats.GetPlayerAmmo(), ammoArray);
    }
    

    void StatChange(int i, Image[] imageArray)
    {
        for (int j = 0; j < imageArray.Length; j++)
        {
            imageArray[j].sprite = (j < i) ? fullSprite : brokenSprite;
        }
    }
}
