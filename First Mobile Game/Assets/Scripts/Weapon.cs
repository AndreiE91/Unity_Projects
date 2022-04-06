using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    AudioSource shoot;
    public Transform firePoint;
    public Transform firePointLeft;
    public Transform firePointRight;
    public Transform firePointSideLeft;
    public Transform firePointSideRight;
    public GameObject bulletPrefab;
    public CameraFollow cameraFol;
    [HideInInspector]
    public float fireRate = 0.3f;
    public Boost boost;
    public bool initiatedBoost = false;
    public bool motherShip = false;
    public bool isTurnedOff = false;
    private int randIndex;
    private int cooldown = 0;

    private void Start()
    {
        shoot = GetComponent<AudioSource>();
        /*
        if (!isTurnedOff)
        {
            if (!motherShip)
                InvokeRepeating("Shoot", 1.5f, fireRate);
            if (motherShip)
                InvokeRepeating("Shoot", 1.5f, fireRate / 1.5f);
        }
        */
        InvokeRepeating("decreaseCooldown",0,0.1f);
    }

    void decreaseCooldown()
    {
        if(cooldown > 0)
            cooldown--;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && cooldown == 0)
        {
            Shoot();
        }

        /*if (!isTurnedOff)
        {
            if (boost.isBoosted && !initiatedBoost)
            {
                CancelInvoke();
                fireRate /= 2;
                if (!motherShip)
                    InvokeRepeating("Shoot", 0f, fireRate);
                if (motherShip)
                    InvokeRepeating("Shoot", 0f, fireRate / 1.5f);
                initiatedBoost = true;
            }
            if (!boost.isBoosted && initiatedBoost)
            {
                CancelInvoke();
                fireRate *= 2;
                if (!motherShip)
                    InvokeRepeating("Shoot", 0f, fireRate);
                if (motherShip)
                    InvokeRepeating("Shoot", 0f, fireRate / 1.5f);
                initiatedBoost = false;

            }
        }
        */


    }

    void Shoot()
    {
            shoot.Play(0);
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            cooldown = 1;
            /*
                if (cameraFol.playerObj.activeSelf)
                {
                    if (boost.isBoosted)
                    {
                        Instantiate(bulletPrefab, firePointLeft.position, firePoint.rotation);
                        Instantiate(bulletPrefab, firePointRight.position, firePoint.rotation);
                    }
                    if (motherShip)
                    {
                        randIndex = Random.Range(0, 2);
                        switch (randIndex)
                        {
                            case 0:
                                {
                                    Instantiate(bulletPrefab, firePointLeft.position, firePointLeft.rotation);
                                    break;
                                }
                            case 1:
                                {
                                    Instantiate(bulletPrefab, firePointRight.position, firePointRight.rotation);
                                    break;
                                }
                                /*
                            case 2:
                                {
                                    Instantiate(bulletPrefab, firePointSideLeft.position, firePointSideLeft.rotation);
                                    break;
                                }
                            case 3:
                                {
                                    Instantiate(bulletPrefab, firePointSideRight.position, firePointSideRight.rotation);
                                    break;
                                }

                        }
                    }
            */
        }
    }






