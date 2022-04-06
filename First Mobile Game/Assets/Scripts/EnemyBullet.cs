using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public float speed = 60f;
    public int damage = 30;
    public static float timeOnScreen = 1.0f;
    public Rigidbody2D rb;
    public GameObject impactEffect;
    public Rigidbody2D playerRb;

    void Start()
    
    {
        rb.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Player"))
        {
            Player player = hitInfo.GetComponent<Player>();
            playerRb = GameObject.Find("PlayerCollider").GetComponent<Rigidbody2D>();
            playerRb.velocity = new Vector2(0, 0);
            if (player.shieldHP <= 0)
                player.currentHealth -= damage;
            else
                player.shieldHP -= damage;
            player.tookDmg = true;
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }


}
