using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public CameraShake cs;

    //private float minX = -96.5f, maxX = 96.5f, minY = -70.5f, maxY = 70.5f;

    [SerializeField]
    private Transform player;
    public Vector3 tempPos;
    public AudioSource playerDeath;
    private bool isDead = false;
    public Mothership mother;
    public GameObject playerObj;
    public bool motherShip = false;

    
    void Update()
    {
            if (!playerObj.activeSelf)
            {
                if (!isDead)
                {
                    playerDeath.Play(0);
                    isDead = true;
                }
            }
        if (!motherShip)
        {
            if (mother.hasRespawned)
            {
                isDead = false;
                transform.position = tempPos;
            }
        }
        
    }
    
    
    void LateUpdate()
    {

        if (!player)
            return;
        tempPos = transform.position;
        tempPos.x = player.position.x;
        tempPos.y = player.position.y;

        /*
        if (tempPos.x < minX)
            tempPos.x = minX;
        if (tempPos.x > maxX)
            tempPos.x = maxX;

        if (tempPos.y < minY)
            tempPos.y = minY;
        if (tempPos.y > maxY)
            tempPos.y = maxY;

        if(!cs.shaking)
        transform.position = tempPos;
        */
    }
}
