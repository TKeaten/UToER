using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrab : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    Animator animator;
    //CharacterController2D player;
    public int health = 100;
    public GameObject deathEffect;
    public float speed = 0.4f;
    public float changeTimer = 3.0f;
    int direction = 1;
    float timer;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //timer = changeTimer;
    }

    /*void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTimer;
        }

        Vector2 position = rigidbody2d.position;
        position.x = position.x + Time.deltaTime * speed * direction;
        animator.SetFloat("Move X", direction);
        rigidbody2d.MovePosition(position); 
        Vector2 position = rigidbody2d.position;
        
        animator.SetFloat("Move X", direction);
        rigidbody2d.MovePosition(position);
    }*/

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
