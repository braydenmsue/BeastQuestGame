using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerCombat : MonoBehaviour
{
    
     //number of stacks of bleed applied on hit, once 10 stacks are applied, next attack multiply attack damage by 5

    public GameObject player;

    public BoxCollider2D playerHitbox;
    public BoxCollider2D aimHitbox;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public Rigidbody2D rigidBody;
    private Vector2 knockbackForce;
    public float knockbackMultiplier;

    [SerializeField] private float invincibilityDuration;
    private bool isInvincible = false;
    public int maxHealth;
    public int currentHealth;
    public float attackRange;
    public int attackDamage;
    public float attackSpeed;
    public float attackTime;

    // BLEED ATTACK AMPLIFIER
    private bool bleedCheck = false;
    public int bleedAttack = 0;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerHitbox = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        currentHealth = maxHealth;
    }
    // Update is called once per frame
    void Update()
    {
      if(Time.time > attackTime)
        {
            if (GameObject.Find("Player").transform.position.y < 0.0f)
            {
                die();
            }
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                attackTime = Time.time + 1f / attackSpeed;
            }
        }
   
    }

    void invincibilityTrigger()
    {
        if (!isInvincible)
        {
            StartCoroutine(becomeInvincible());
        }
    }
    private IEnumerator becomeInvincible()
    {
        isInvincible = true;

        yield return new WaitForSeconds(invincibilityDuration);

        isInvincible = false;
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        RaycastHit2D enemyBelow = Physics2D.BoxCast(playerHitbox.bounds.center, playerHitbox.bounds.size, 0f, Vector2.down, 2f, enemyLayers);
        if (detectEnemy())
        {
            if (enemyBelow.collider != null)
            {
                Debug.Log("Down Attack");
                player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 40, ForceMode2D.Impulse);
            }
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.GetComponent<EnemyCombat>().bleedStacks>= 10) { bleedCheck = true; } else { bleedCheck = false; }
                if (!bleedCheck)
                {
                    enemy.GetComponent<Rigidbody2D>().AddForce(knockbackForce, ForceMode2D.Impulse);
                    enemy.GetComponent<EnemyCombat>().takeDamage(attackDamage);
                    enemy.GetComponent<EnemyCombat>().bleedStacks += bleedAttack;
                }
                else
                {
                    enemy.GetComponent<Rigidbody2D>().AddForce(2*knockbackForce, ForceMode2D.Impulse);
                    enemy.GetComponent<EnemyCombat>().takeDamage(attackDamage*5);
                    enemy.GetComponent<EnemyCombat>().bleedStacks =0 ;
                }
                
            }
        }

    }

    void die()
    {
        Debug.Log("Player Died");
    }

    public void takeDamage(int damage)
    {
        if (isInvincible)
        {
            return;
        }
        Debug.Log(damage + " damage taken");
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            die();
        }

        StartCoroutine(becomeInvincible());
    }

    bool detectEnemy()
    {
        RaycastHit2D AttackL = Physics2D.BoxCast(playerHitbox.bounds.center, playerHitbox.bounds.size, 0f, Vector2.left, 5f, enemyLayers);
        RaycastHit2D AttackR = Physics2D.BoxCast(playerHitbox.bounds.center, playerHitbox.bounds.size, 0f, Vector2.right, 5f, enemyLayers);
        RaycastHit2D AttackD = Physics2D.BoxCast(playerHitbox.bounds.center, playerHitbox.bounds.size, 0f, Vector2.down, 5f, enemyLayers);

        if (AttackL.collider != null)
        {
            knockbackForce = (transform.up * -knockbackMultiplier + transform.right * knockbackMultiplier);
            return AttackL.collider != null;
        }
        else if(AttackR.collider != null)
        {
            knockbackForce = (transform.up * knockbackMultiplier + transform.right * knockbackMultiplier);
            return AttackR.collider != null;
        }
        else if(AttackD.collider != null)
        {
            knockbackForce = Vector2.zero;
            return AttackD.collider != null;
        }
        return false;
    }
}
