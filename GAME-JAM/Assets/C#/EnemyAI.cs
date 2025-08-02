using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    [SerializeField] GameObject _projectile;
    [SerializeField] Transform _spawnLoc;

    private bool isAttacking;
    private bool isFacingRight = true;
    private Rigidbody2D rb;
    private float maxRange = 1;
    private float minRange = -1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _spawnLoc.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
            isAttacking = !isAttacking;
        Flip();
    }
    private void FixedUpdate()
    {
        EnemyBehavior();
    }
    private void EnemyBehavior()
    {
        if (isAttacking)        //In Attacking State
        {
            Vector2 Direction = Movement.player.gameObject.transform.position - transform.position;
            Direction.y = 0;
            Direction.Normalize();
            rb.velocity = new Vector2 (Direction.x * _speed, 0);

            //Instantiate()
        }
        else                    //In Idle State
        {
            
            Vector2 Direction = _spawnLoc.position - transform.position;
            Direction.y = 0;
            Direction.Normalize();
            rb.velocity = new Vector2(Direction.x * _speed, 0);
        }
    }

    private void Flip()
    {
        if (isFacingRight && rb.velocity.x < 0f || !isFacingRight && rb.velocity.x > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
