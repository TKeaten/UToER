using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * General note - I'm slamming way too much into one class.
 */
 
public class Jump : MonoBehaviour
{
    public float speed;                                 // How fast the character is moving
    public float jumpForce;                             // How high the character is jumping

    public int maxHealth = 5;                           // Player max health
    public int health { get { return currentHealth; } } // Property - i.e. a function that operates like a variable.
    int currentHealth;
    public float timeInvincible = 2.0f;                 // How long the player stays invincible once receiving damage;
    bool isInvincible;                                    // Whether the player is invincible right now or not
    float invincibleTimer;                              // How long the invincibility has remaining

    private Rigidbody2D rb;                             // For movement AddFoce() method
    private SpriteRenderer sprite;                      // For facing the right direction
    private Animator anim;                              // For animation

    public bool isGrounded;                             // Checks to see if the character is on the ground

    int layerMask;                                      // Used to hold the names of layers

    public Transform firePoint;                         // Where our bullet will originate
    public GameObject bulletPrefab;                     // The bullet object we're going to fire

    private bool facingDirection = true;                // For determining which way the player is currently facing.
    private GameObject boxRef;                          // The sprite that roughly shows the boxcast; optional

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        isGrounded = true;
        layerMask = LayerMask.GetMask("Collision", "Default");
        currentHealth = maxHealth;
        boxRef = GameObject.Find("BoxReference");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            /* Raycast shooting
             * StartCoroutine(Shoot());
             */
        }
    }

    void FixedUpdate()
    {
        // Get player's left or right input
        float horiz = Input.GetAxis("Horizontal");

        // Have character face correct direction
        if (horiz > 0 && !facingDirection)
        {
            // ... flip the player.
            Flip();
        } else if (horiz < 0 && facingDirection)
        {
            // ... flip the player.
            Flip();
        }

        Vector2 targetVelocity = Vector2.right * horiz * speed;
        Vector2 refVelocity = Vector2.zero;
        Vector2 dampedVelocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref refVelocity, .05f);
        rb.velocity = dampedVelocity;
        anim.SetFloat("Speed",  Mathf.Abs(horiz * speed));

        RaycastHit2D hitInfo;
        /*
         * Originates a raycast from the center bottom (with a little extra to not hit the sprite) that points down by 0.1 and only returns true when detecting layerMask values.
         * hitInfo = Physics2D.Raycast(transform.position - new Vector3(0, sprite.bounds.extents.y - .3f, 0), Vector2.down, 0.3f, layerMask);
         * Debug.Log(hitInfo);
         */

        // Creating a boxcast
        Vector2 boxSize = new Vector2(0.25f, 0.1f);
        hitInfo = Physics2D.BoxCast(transform.position - new Vector3(0, sprite.bounds.extents.y - boxSize.y, 0), boxSize, 0, Vector2.down, boxSize.y, layerMask);
        // Set the reference sprite to where the boxcast is
        boxRef.transform.position = transform.position - new Vector3(0, sprite.bounds.extents.y - boxSize.y, 0);
        boxRef.transform.localScale = boxSize;
        if (hitInfo)
        {
            Debug.Log("Touching the ground");
            isGrounded = true;
        } else
        {
            Debug.Log("In the air");
            isGrounded = false;
        }

        Debug.DrawRay(transform.position - new Vector3(0, sprite.bounds.extents.y - .3f, 0), Vector2.down * 0.3f, Color.red);

        // If the player presses the jump button and is on the ground, then jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(jumpForce * Vector2.up);
        }

        // Play correct animation
        anim.SetBool("IsJumping", !isGrounded);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }
    }

    public void ChangeHealth (int amount)
    {
        if (amount > 0)
        {
            if (isInvincible)
                return;
            anim.SetTrigger("Hit");
            // hitEffect.Play();  This is using the particle system to display some particles on hit
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        PlayerHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        anim.SetBool("Shooting", true);
    }

    void AnimationEnded ()
    {
        anim.SetBool("Shooting", false);
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingDirection = !facingDirection;
        transform.Rotate(0, 180, 0);
    }
}
