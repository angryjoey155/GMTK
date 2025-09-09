using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour
{
    private List<GameObject> enemies = new List<GameObject>();

    private void OnTriggerStay2D(Collider2D collision)
    {

        //Debug.Log(ray.collider.name);
        if (enemies.Contains(collision.gameObject))
            return;

        Vector3 direction = Movement.player.transform.position - collision.transform.position;
        RaycastHit2D ray = Physics2D.Raycast(collision.transform.position, direction);
        Debug.DrawRay(collision.transform.position, direction);
        Debug.Log(ray.collider.CompareTag("Ground"));

        
        if (collision.gameObject.CompareTag("enemy"))
        {
            //Debug.Log(collision.gameObject.name);
            enemies.Add(collision.gameObject);
        }
        if (ray.collider.CompareTag("Ground"))
        {
            if (enemies.Contains(collision.gameObject))
                enemies.Remove(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
            enemies.Remove(collision.gameObject);
    }
    public List<GameObject> getAllEnemies()
    {
        return enemies;
    }
}
