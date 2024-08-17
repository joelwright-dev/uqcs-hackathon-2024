using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] public float playerSpeed = 5f;
    [SerializeField] public float jumpForce = 10f;
    [SerializeField] public float jumpPadForce = 11;
    [SerializeField] public float maxSpeed = 10f;
    public Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isFacingRight = true;
    public bool isGrounded = false;
    private Rigidbody2D rigidBody;
    private Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        rigidBody.velocity = new Vector2(moveHorizontal * playerSpeed, rigidBody.velocity.y);
        rigidBody.velocity = Vector2.ClampMagnitude(rigidBody.velocity, maxSpeed);
        
        if (Input.GetKeyDown(KeyCode.W) && isGrounded) {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

        animator.SetBool("isRunning", moveHorizontal != 0);
        animator.SetBool("isGrounded", isGrounded);

        if (moveHorizontal > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveHorizontal < 0 && isFacingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        spriteRenderer.flipX = !isFacingRight;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Jump Pad") {
            rigidBody.AddForce(Vector2.up * jumpPadForce, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
        // this.velocity.y = 0;
    }
}
