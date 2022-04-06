using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 60f;
    public int damage = 40;
    public Rigidbody2D rb;
    public GameObject impactEffect;
    
    void Start()
    {
        rb.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Enemy"))
        {
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (hitInfo.CompareTag("Guard"))
        {
            LootGuard guard = hitInfo.GetComponent<LootGuard>();
            if (guard != null)
            {

                guard.TakeDamage(damage);

            }

            Instantiate(impactEffect, transform.position, transform.rotation);


            Destroy(gameObject);
        }
        if (hitInfo.CompareTag("Mine"))
        {
            Mine mine = hitInfo.GetComponent<Mine>();
            if (mine != null)
            {

                mine.TakeDamage(damage);

            }

            Instantiate(impactEffect, transform.position, transform.rotation);


            Destroy(gameObject);
        }

        if (hitInfo.CompareTag("Mother") || hitInfo.CompareTag("Base"))
            Destroy(gameObject);

        if (hitInfo.CompareTag("Mother") || hitInfo.CompareTag("Base"))
            Destroy(gameObject);
    }

}
