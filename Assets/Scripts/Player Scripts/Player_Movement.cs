using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;
    public Animator animator;
    public Rigidbody2D playerBody2D;
    public BoxCollider2D playerHitBox;

    public float horizInput;
    public float movementSpeed;
    public float jumpHeight;

    // Start is called before the first frame update
    void Awake()
    {
        playerBody2D = GetComponent<Rigidbody2D>();
        playerHitBox = GetComponent<BoxCollider2D>();
    }
    
    // Check if player is on the ground
    private bool onGround()
    {
        RaycastHit2D touchGround = Physics2D.BoxCast(playerHitBox.bounds.center, playerHitBox.bounds.size, 0f, Vector2.down, 0.1f, groundLayerMask);
        return touchGround.collider != null;
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreLayerCollision(6, 9);
        // Set animator variables
        animator.SetFloat("Speed", horizInput);
        animator.SetBool("Jump", false);

        // Move forward and backward
        horizInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector2.right * Time.deltaTime * movementSpeed * horizInput);
        

        // Jump
        if(onGround() && Input.GetKeyDown(KeyCode.Space))
        {
            playerBody2D.velocity = Vector2.up * jumpHeight;
            animator.SetBool("Jump", true);
        }
    }
}
