using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float health;
    public decimal scoreReward = 1000;
    public GameObject deathEffect;
    [HideInInspector]
    public ScoringSystem killCount;

    public GameObject enemyBaby;

    private EnemyAI enemyAI;
    private AsteroidAI asteroidAI;

    public bool boss = false;
    public bool asteroid;
    public float speedModifier = 0.6f;

    private Spawning spawner;

    [HideInInspector]
    public float speed = 0f;
    [HideInInspector]
    public float vertSpeed = 0f;
    private Rigidbody2D enemyBody;
    private CameraFollow camScript;

    void Awake()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        if(!asteroid)
            enemyAI = GetComponent<EnemyAI>();
        else
            asteroidAI = GetComponent<AsteroidAI>();
    }

    void Start()
    {
        camScript = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        spawner = GameObject.Find("Spawner").GetComponent<Spawning>();

        health *= spawner.difficulty;
        scoreReward *= (decimal)(spawner.difficulty);
        if(!asteroid)
            enemyAI.moveSpeed += (enemyAI.moveSpeed*spawner.difficulty - enemyAI.moveSpeed)/2;
        else
            asteroidAI.speedMultiplier += (asteroidAI.speedMultiplier * spawner.difficulty - asteroidAI.speedMultiplier) / 2;
        //Debug.Log("Health: " + health);
        //speedModifier *= spawner.difficulty;
        killCount = GameObject.Find("Canvas").GetComponentInChildren<ScoringSystem>(); ;
    }


    void FixedUpdate()
    {
        enemyBody.velocity = new Vector2(speed * speedModifier, vertSpeed * speedModifier);
        
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
        killCount.AddKill(scoreReward);
        killCount.showKills();
        bool isCritical;
        decimal fundsReward = scoreReward * 0.001m;
        if (fundsReward >= 5)
            isCritical = true;
        else
            isCritical = false;
        FundsPopup.Create(transform.position + new Vector3(6.5f,1), fundsReward, isCritical);
        if (boss)
        {
            Instantiate(enemyBaby, transform.position + new Vector3(0, 3, 0), Quaternion.identity);
            Instantiate(enemyBaby, transform.position + new Vector3(0, -3, 0), Quaternion.identity);
            Instantiate(enemyBaby, transform.position + new Vector3(3, 0, 0), Quaternion.identity);
            Instantiate(enemyBaby, transform.position + new Vector3(-3, 0, 0), Quaternion.identity);
            Instantiate(enemyBaby, transform.position + new Vector3(3, 3, 0), Quaternion.identity);
            Instantiate(enemyBaby, transform.position + new Vector3(-3, -3, 0), Quaternion.identity);
            Instantiate(enemyBaby, transform.position + new Vector3(3, -3, 0), Quaternion.identity);
            Instantiate(enemyBaby, transform.position + new Vector3(-3, 3, 0), Quaternion.identity);
            Instantiate(enemyBaby, transform.position + new Vector3(-3, 3, 0), Quaternion.identity);
        }
        Destroy(gameObject);

    }

    

}
