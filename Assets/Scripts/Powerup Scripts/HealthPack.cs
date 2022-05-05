using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{

    [SerializeField] private GameObject player;
    public int health;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameObject player = collision.gameObject;
            player.GetComponent<PlayerCombat>().currentHealth += health;
            Destroy(gameObject);
        }
    }
}
