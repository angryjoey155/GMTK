using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillAura : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        Invoke("DeleteSelf", 0.2f);
    }
    void DeleteSelf()
    {
        Destroy(this.gameObject);
    }
}
