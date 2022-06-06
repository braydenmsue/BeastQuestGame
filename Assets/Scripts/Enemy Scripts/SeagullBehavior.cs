using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullBehavior : MonoBehaviour
{
    public GameObject player;
    public GameObject playerCombat;
    public Transform playerPosition;
    public LayerMask playerLayer;

    public LayerMask ignoreLayer;


    public Animator animator;
    public Rigidbody2D seagullRB;
    public BoxCollider2D seagullHitBox;

    public float knockbackMultiplier;

    [SerializeField] private int minDistance = 1;
    public float movementSpeed;
    public int collisionDamage = 5;
    private Vector2 knockbackForce;



    // Start is called before the first frame update
    void Awake()
    {
        seagullHitBox = GetComponent<BoxCollider2D>();
        seagullRB = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {

        if (animator.GetBool("Dead") == true)
        {
            movementSpeed = 0; //stop moving
        }
        if (detectPlayer())
        {
            //if collision happens between player and seagull, deal damage and knock them back
            player.GetComponent<Rigidbody2D>().AddForce(knockbackForce, ForceMode2D.Impulse);
            playerCombat.GetComponent<PlayerCombat>().takeDamage(collisionDamage);
        }


        
        float dist = Vector3.Distance(playerPosition.position, transform.position);

        if (dist <= minDistance)
        { 
            transform.position = Vector3.MoveTowards(transform.position, playerPosition.transform.position, movementSpeed * Time.deltaTime);
        }

    }

    //raycast left, right, and up to detect which direction knockback will send players on collision
    bool detectPlayer()
    {
        RaycastHit2D AttackL = Physics2D.BoxCast(seagullHitBox.bounds.center, seagullHitBox.bounds.size, 0f, Vector2.left, 0.1f, playerLayer);
        RaycastHit2D AttackR = Physics2D.BoxCast(seagullHitBox.bounds.center, seagullHitBox.bounds.size, 0f, Vector2.right, 0.1f, playerLayer);
        RaycastHit2D AttackU = Physics2D.BoxCast(seagullHitBox.bounds.center, seagullHitBox.bounds.size, 0f, Vector2.up, 0.1f, playerLayer);

        if (AttackL.collider != null)
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
}

    

