using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUp : MonoBehaviour
{

    public float speed = 5f;
    public int damage = 25;
    public Rigidbody2D rb;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.up * speed;
        StartCoroutine(TimeDestroy());
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyCrab enemy = hitInfo.GetComponent<EnemyCrab>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    IEnumerator TimeDestroy ()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
