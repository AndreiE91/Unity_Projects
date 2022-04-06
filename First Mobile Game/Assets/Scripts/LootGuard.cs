using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootGuard : MonoBehaviour
{
    private Transform target;
    private Rigidbody2D enemyBody;
    public AudioSource shoot;
    public Transform firePoint;
    public Transform firePointLeft;
    public Transform firePointRight;
    public GameObject bulletPrefab;
    public CameraFollow cameraFol;

    public int health = 2500;
    public int scoreReward = 5000;
    public GameObject deathEffect;
    [HideInInspector]
    public ScoringSystem killCount;

    public float fireRate = 1f;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("checkDistance", 0f, 1.5f);
        target = GameObject.Find("PlayerCollider").transform;
        enemyBody = GetComponent<Rigidbody2D>();
        shoot = GetComponent<AudioSource>();
        InvokeRepeating("Shoot", 0.5f, fireRate);
        killCount = GameObject.Find("KillCountText").GetComponent<ScoringSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (distance < 80)
        {
            Vector3 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            enemyBody.rotation = angle - 90;
            direction.Normalize();
        }
    }

    void checkDistance()
    {
        distance = Vector3.Distance(transform.position, cameraFol.playerObj.transform.position);
    }

    void Shoot()
    {
        if (cameraFol.playerObj.activeSelf && distance < 80)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Instantiate(bulletPrefab, firePointLeft.position, firePointLeft.rotation);
            Instantiate(bulletPrefab, firePointRight.position, firePointRight.rotation);
            shoot.Play(0);
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

        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        killCount.AddKill(scoreReward);
        killCount.showKills();

    }
}
