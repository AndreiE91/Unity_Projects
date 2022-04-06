using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject cornerGrey;
    public GameObject cornerColliderObj;
    public GameObject cornerTriggerObj;
    public GameObject deathEffect;
    public int cornerHP = 1000;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            
            Enemy enemyScript = col.GetComponent<Enemy>();

            int tempHealth = cornerHP;
            cornerHP -= (int)(enemyScript.health / 2);
            
            enemyScript.TakeDamage(tempHealth * 2);
            if (cornerHP <= 0)
            {
                cornerTriggerObj.SetActive(false);
                cornerColliderObj.SetActive(false);
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                cornerGrey.SetActive(true);
            }
        }
    }

}
