using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckBehavior : MonoBehaviour
{
    public GameObject player;
    public GameObject playerCombat;

    public LayerMask playerLayer;
    public LayerMask edgeLayerMask;
    public LayerMask groundLayerMask;
    public LayerMask ignoreLayer;

    public Animator animator;
    public BoxCollider2D duckHitBox;

    public float knockbackMultiplier;
    private int direction = 1;
    public float movementSpeed;
    public int collisionDamage = 15;
    private Vector2 knockbackForce;



    // Start is called before the first frame update
    void Awake()
    {
      duckHitBox = GetComponent<BoxCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if(animator.GetBool("Dead") == true)
        {
            direction = 0;
        }
        if (detectPlayer())
        {
            player.GetComponent<Rigidbody2D>().AddForce(knockbackForce, ForceMode2D.Impulse);
            playerCombat.GetComponent<PlayerCombat>().takeDamage(collisionDamage);
        }
        if (detectEdge() != true)
        {
            transform.Translate(Vector2.left * Time.deltaTime * direction * movementSpeed);
        }
        if (detectEdge())
        {
            direction *= -1;
            if(animator.GetBool("Direction") == true)
            {
                animator.SetBool("Direction", false);
            }
            else
            {
                animator.SetBool("Direction", true);
            }
            for (int i = 0; i < 30; i++)
            {
                transform.Translate(Vector2.left * Time.deltaTime * direction);
            }
        }
    }
    
    bool detectPlayer()
    {
        RaycastHit2D AttackL = Physics2D.BoxCast(duckHitBox.bounds.center, duckHitBox.bounds.size, 0f, Vector2.left, 0.1f, playerLayer);
        RaycastHit2D AttackR = Physics2D.BoxCast(duckHitBox.bounds.center, duckHitBox.bounds.size, 0f, Vector2.right, 0.1f, playerLayer);
        RaycastHit2D AttackU = Physics2D.BoxCast(duckHitBox.bounds.center, duckHitBox.bounds.size, 0f, Vector2.up, 0.1f, playerLayer);

        if(AttackL.collider != null)
        {
            knockbackForce = (transform.up * knockbackMultiplier + transform.right * -knockbackMultiplier);
            return AttackL.collider != null;
        }
        else if (AttackR.collider != null)
        {
            knockbackForce = (transform.up * knockbackMultiplier + transform.right * knockbackMultiplier);
            return AttackR.collider != null;
        }
        else if (AttackU.collider != null)
        {
            knockbackForce = (transform.up * knockbackMultiplier + transform.right * knockbackMultiplier);
            return AttackU.collider != null;
        }
        return false;
    }

    bool detectEdge()
    {
        RaycastHit2D contactL = Physics2D.BoxCast(duckHitBox.bounds.center, duckHitBox.bounds.size, 0f, Vector2.left, 0.1f, edgeLayerMask, ~ignoreLayer);
        RaycastHit2D contactR = Physics2D.BoxCast(duckHitBox.bounds.center, duckHitBox.bounds.size, 0f, Vector2.right, 0.1f, edgeLayerMask, ~ignoreLayer);      

        if (contactR.collider != null)
        {
            return contactR.collider != null;
        }
        else if (contactL.collider != null)
        {
            return contactL.collider != null;
        }

        return false;
    }
}

