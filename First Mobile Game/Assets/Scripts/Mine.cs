using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    private int health = 40;
    private int tempHealth;
    public GameObject explosionEffect;
    private Rigidbody2D mineBody;
    public Transform N, S, E, W, NE, NW, SE, SW;
    public GameObject shrapnel;
    public MineSpawner mineSpawner;

    void Start()
    {
        mineSpawner = GameObject.FindWithTag("SpawnMines").GetComponent<MineSpawner>();
        mineBody = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            tempHealth = col.GetComponent<Player>().currentHealth;
            col.GetComponent<Player>().currentHealth -= health;
            col.GetComponent<Player>().tookDmg = true;
            TakeDamage(tempHealth);
           
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {

            Die();

        }
    }

    void Die()
    {

        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Instantiate(shrapnel, N.position, N.rotation);
        Instantiate(shrapnel, S.position, S.rotation);
        Instantiate(shrapnel, E.position, E.rotation);
        Instantiate(shrapnel, W.position, W.rotation);
        Instantiate(shrapnel, NE.position, NE.rotation);
        Instantiate(shrapnel, NW.position, NW.rotation);
        Instantiate(shrapnel, SE.position, SE.rotation);
        Instantiate(shrapnel, SW.position, SW.rotation);
        Destroy(gameObject);
        mineSpawner.mineCount--;

    }


}
