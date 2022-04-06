using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform target;
    private Vector2 movement;
    private Rigidbody2D enemyBody;
    private CameraFollow camScript;
    public bool followPlayer = true;

    private Rigidbody2D targetSwitch;

    public float moveSpeed = 5f;

    void Awake()
    {
        camScript = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        if (camScript.motherShip)
        targetSwitch = GameObject.Find("TargetSwitch").GetComponent<Rigidbody2D>();
        camScript = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
    }

    void Start()
    {
        
        
        if (!camScript.motherShip)
        {
            target = GameObject.Find("GameManager").transform;
        }
        else
        {
            moveSpeed *= 2.5f;
            if (followPlayer)
                target = GameObject.Find("PlayerCollider").transform;
            else
            {
                target = GameObject.Find("GameManager").transform;
            }

        }
        enemyBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
            Vector3 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            enemyBody.rotation = angle;
            direction.Normalize();
            movement = direction;
    }

    void FixedUpdate()
    {
        moveEnemy(movement);
    }

    void moveEnemy(Vector2 direction)
    { 
            enemyBody.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("tSwitch"))
        {
            target = GameObject.Find("PlayerCollider").transform;
        }
    }

}
