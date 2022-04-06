using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PlayerWeapon : MonoBehaviour
{

    public event EventHandler<OnShootEventArgs> OnShoot;

    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
    }

    [SerializeField] private Material weaponTracerMaterial;
    [SerializeField] private Sprite shootFlashSprite;


    public Buttons buttonScript;
    public Transform playerPos;
    public Transform aimTransform;
    public AudioSource shoot;
    public Transform firePoint;
    public GameObject bulletPrefab;
    private int cooldown = 0;
    public float fireRate = 1f;
    public GameObject flash;
    private Vector3 aimDirMem;
    private Vector3 touchPosMem;


    private void Awake()
    {
        shoot = GetComponent<AudioSource>();
        InvokeRepeating("decreaseCooldown", 0, 0.1f);
    }

    private void Update()
    {
        HandleAiming();
        if (Input.touchCount > 0 && cooldown == 0 && Time.timeScale != 0 && buttonScript.brake)
        {
            Shoot();
        }
    }

    void decreaseCooldown()
    {
        if (cooldown > 0)
            cooldown--;
    }

    private void HandleAiming()
    {
        if (Input.touchCount > 0)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            touchPosition.z = 0;
            touchPosMem = touchPosition;
            Vector3 aimDirection = (touchPosition - transform.position).normalized;
            aimDirMem = aimDirection;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            aimTransform.eulerAngles = new Vector3(0, 0, angle);
        }
    }

    private void CreateWeaponTracer(Vector3 fromPos, Vector3 targetPos)
    {
        Vector3 dir = (targetPos - fromPos).normalized;
        float eulerZ = UtilsClass.GetAngleFromVectorFloat(dir) - 90;
        float distance = Vector3.Distance(fromPos, targetPos);
        Vector3 tracerSpawnPos = fromPos + dir * 60f * 0.5f;
        World_Mesh worldMesh = World_Mesh.Create(tracerSpawnPos, eulerZ, 0.15f, 60f, weaponTracerMaterial, null, 10000);

        int frame = 0;
        float framerate = 0.016f;

        float timer = framerate;
        worldMesh.SetUVCoords(new World_Mesh.UVCoords(0,0,4,443));
        FunctionUpdater.Create( () => {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                frame++;
                timer += framerate;
                if (frame >= 4)
                {
                    worldMesh.DestroySelf();
                    return true;
                }
                else
                {
                    worldMesh.SetUVCoords(new World_Mesh.UVCoords(4 * frame, 0, 4, 443));
                }
            }
            return false;
        });
    }

    void Shoot()
    {
        shoot.Play(0);
        UtilsClass.ShakeCamera(.2f, .03f);
        CreateWeaponTracer(firePoint.position, touchPosMem);
        CreateShootFlash(firePoint.position);
        OnShoot?.Invoke(this, new OnShootEventArgs
        {
            gunEndPointPosition = firePoint.position,
            shootPosition = touchPosMem,
        }); 
        StartCoroutine(muzzleFlash());
        aimTransform.position -= aimDirMem/2;

        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        cooldown = 1;
        StartCoroutine(handleRecoil());
    }

    void CreateShootFlash(Vector3 spawnPosition)
    {
        World_Sprite worldSprite = World_Sprite.Create(spawnPosition, shootFlashSprite);
        worldSprite.SetSortingLayerName("UI");
        worldSprite.SetColor(Color.white - new Color(0,0,0,0.5f));
        FunctionTimer.Create(worldSprite.DestroySelf, 0.1f);
    }

    IEnumerator muzzleFlash()
    {
        flash.SetActive(true);
        yield return new WaitForSeconds(0.025f);
        flash.SetActive(false);
    }

    IEnumerator handleRecoil()
    {
        yield return new WaitForSeconds(0.025f);
        aimTransform.position = playerPos.position;
    }

}
