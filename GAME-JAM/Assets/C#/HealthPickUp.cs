using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public float floatStrength = 0.5f; 
    public float floatSpeed = 1f;      
    [SerializeField] AudioClip HealAC;

    private float originalY;

    void Start()
    {
        originalY = transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector2(transform.position.x, originalY + (Mathf.Sin(Time.time * floatSpeed) * floatStrength));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(HealAC, transform.position);
        PlayerStats.ChangeHealth(5);
        Destroy(gameObject);
    }
}