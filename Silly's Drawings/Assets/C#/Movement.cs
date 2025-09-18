using UnityEngine;

public class Movement : MonoBehaviour
{
    public static GameObject player;
    [SerializeField] private GameObject setPlayer;

    private UnityEngine.Animator animator;

    private float horizontal;
    [SerializeField] public float PlayerSpeed = 2f;
    [SerializeField] private float jumpingPower = 16f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private float speedCap = 0f;

    private void Awake()
    {
        player = setPlayer;

    }
    private void Start()
    {
        animator = GetComponent<UnityEngine.Animator>();
        animator.speed = 2; //2X the animation speed
    }

    void Update()
    {

        if (PlayerStats.GetIsDead())
        {
            baloon = false;
            rb.gravityScale = 4;
            return;
        }

        //used to update movement
        horizontal = Input.GetAxisRaw("Horizontal");

        floatBollon();
        Jumping();
        Flip();
    }
    bool baloon;
    private void floatBollon()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {//sets gravity to negative
            baloon = true;
            rb.gravityScale = -1f;
            rb.velocity = rb.velocity /2.5f;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            baloon= false;
            rb.gravityScale = 4;
        }
        if (!baloon)
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

        rb.velocity += new Vector2(horizontal * PlayerSpeed, 0);
        if (rb.velocity.x >= speedCap || rb.velocity.x <= -speedCap)
        {
            float cappedSpeed = Mathf.Abs(rb.velocity.x);
            cappedSpeed /= 1.1f;
            cappedSpeed *= Mathf.Sign(rb.velocity.x);

            rb.velocity = new Vector2(cappedSpeed, rb.velocity.y); 
        }
        if (IsGrounded()) 
            rb.drag = 7;
        else 
            rb.drag = 2;

        Anims();
    }
    private void Anims()
    {
        if (PlayerStats.GetPlayerHealth() <= 0)
        { 
            animator.Play("Dead");
            rb.gravityScale = 1f;
            return;
        }

        if (baloon)
        {
            animator.speed = 2f;
            animator.Play("Baloon");
        }

        if (rb.velocity != Vector2.zero && !baloon)
        {
            animator.speed = 2f;
            animator.Play("Run");
        }
        else if (!baloon)
        {
            animator.speed = 1f; // makes Idle look a bit better than 2X
            animator.Play("Idle");
        }

    }

    private void OnDeath() //on a animation event
    {
        PauseMenu.thisPauseMenu.DeathScreen();
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
    }
}
