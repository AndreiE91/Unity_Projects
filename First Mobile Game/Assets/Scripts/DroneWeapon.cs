using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneWeapon : MonoBehaviour
{
    public AudioSource shoot;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public CameraFollow cameraFol;
    public float shootOffsetTime = 1.5f;

    private GameObject spawnedBullet;

    public float fireRate = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        shoot = GetComponent<AudioSource>();
        InvokeRepeating("Shoot", shootOffsetTime, fireRate);
    }


    void Shoot()
    {
        if (cameraFol.playerObj.activeSelf)
        {
            if (gameObject.activeSelf)
            {
                spawnedBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                spawnedBullet.transform.parent = firePoint.transform;
                shoot.Play(0);
            }
        }

    }
}
