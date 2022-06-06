using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

    public int bleedStacks = 0;
    private SpriteRenderer enemy;
    public Rigidbody2D rb;
    public Animator animator;
    public int attackDamage = 10;
    public int maxHealth = 100;
    int currentHealth;

    public GameObject floatingPoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update

    void Start()
    {
        
        currentHealth = maxHealth;
        enemy = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {

    }

    public void takeDamage(int damage)
    {

        currentHealth -= damage;
        // display damage taken after a hit 
        GameObject points = Instantiate(floatingPoint, transform.position, Quaternion.identity) as GameObject;
        if (damage >= 100)
        {
            // if the hit is a critical hit, text is red
            points.transform.GetChild(0).GetComponent<TextMesh>().color = Color.red;
        }
        else
        {
            points.transform.GetChild(0).GetComponent<TextMesh>().color = Color.white;
        }
        // display amount of damage being taken 
        points.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();

        if(currentHealth <= 0)
        {
            die();
        }
    }

    void die()
    {
        // stop movement and disable collisions
        animator.SetBool("Dead", true);
        GetComponent<Collider2D>().enabled = false;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
        
        rb.gravityScale = 0;
        enemy.sortingOrder = 1;
        enemy.sortingLayerName = "Background";
        this.enabled = false;
    }
}
