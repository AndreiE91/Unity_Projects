using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidAI : MonoBehaviour
{
    private Transform target;
    private Vector2 movement;
    private Rigidbody2D asteroidBody;
    private bool followPlayer;

    private float moveSpeed;
    public float speedMultiplier = 1f;

    void Awake()
    {
        asteroidBody = GetComponent<Rigidbody2D>();
        moveSpeed = Random.Range(4, 10) * speedMultiplier;
        asteroidBody.angularVelocity = Random.Range(0, 10);
        followPlayer = (Random.value < 0.3f);
        if (followPlayer)
            target = GameObject.Find("PlayerCollider").transform;
        else
            target = GameObject.Find("GameManager").transform;
    }

    void Update()
    {
        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        movement = direction;
        if(target == null)
            target = GameObject.Find("GameManager").transform;
    }

    void FixedUpdate()
    {
        moveAsteroid(movement);
    }

    void moveAsteroid(Vector2 direction)
    {
        asteroidBody.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("tSwitch"))
        {
            target = GameObject.Find("PlayerCollider").transform;
        }
    }

}
