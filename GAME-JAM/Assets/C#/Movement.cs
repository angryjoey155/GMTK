using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    public static GameObject player;
    [SerializeField] private GameObject setPlayer;

    private float horizontal;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private void Awake()
    {
        player = setPlayer;
    }

    void Update()
    {
        //used to update movement
        horizontal = Input.GetAxisRaw("Horizontal");

        floatBollon();
        Jumping();
        Flip();
    }

    private void floatBollon()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {//sets gravity to negative
            rb.gravityScale = -1f;
            rb.velocity = rb.velocity /2.5f;
        }
        if (Input.GetKeyUp(KeyCode.Space))
            rb.gravityScale = 4;
    }

    private void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.W) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetKeyUp(KeyCode.W) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private void FixedUpdate()
    {//Movement
        if (IsGrounded())
            rb.drag = 10f;
        else 
            rb.drag = 0f;
        rb.velocity += new Vector2(horizontal * speed, 0);
        if(rb.velocity.x >= 3f)
            rb.drag = 10f;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    public void AddForce(Vector2 Force)
    {
        rb.velocity = Force;

        //Vector2 force = new Vector2(10f, 0f);
        //rb.AddForce(Force, ForceMode2D.Impulse);
    }
}
