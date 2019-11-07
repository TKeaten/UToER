using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    Animator animator;
    //CharacterController2D player;
    public int health = 100;
    public GameObject deathEffect;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        CharacterController2D player = other.GetComponent<CharacterController2D>();
        
        if (player != null)
        {
            if (player.health > 0)
            {
                player.ChangeHealth(-1);
            }
        }
    }

    public void TakeDamage (int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die ()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
