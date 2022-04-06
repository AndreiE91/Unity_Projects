using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public GameObject barrier;
    public AudioSource barrierDmgSound;

    public GameObject deathEffect;
    private GameObject manager;
    private int barrierHP = 250;

    void Awake()
    {
        manager = GameObject.Find("GameManager");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {

            Enemy enemyScript = col.GetComponent<Enemy>();

            int tempHealth = barrierHP;
            barrierHP -= (int)(enemyScript.health / 2);
            barrierDmgSound.Play(0);

            enemyScript.TakeDamage(tempHealth * 2);
            if (barrierHP <= 0)
            {
                manager.GetComponent<GameManager>().barrierDestroy.Play(0);
                barrier.SetActive(false);
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                

            }
        }
    }

}
