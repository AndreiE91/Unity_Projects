using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class ShootEffects : MonoBehaviour
{
    [SerializeField] private PlayerWeapon playerWeapon;
    [SerializeField] private Material weaponTracerMaterial;

    void Start()
    {
        playerWeapon.OnShoot += PlayerWeapon_OnShoot;
    }

    private void PlayerWeapon_OnShoot(object sender, PlayerWeapon.OnShootEventArgs e)
    {
        UtilsClass.ShakeCamera(1f, .2f);
        
    }

    private void CreateWeaponTracer(Vector3 fromPos, Vector3 targetPos)
    {
        Vector3 dir = (targetPos - fromPos).normalized;
        float eulerZ = UtilsClass.GetAngleFromVectorFloat(dir);
        World_Mesh worldMesh = World_Mesh.Create(fromPos, eulerZ, 6f, 100f, weaponTracerMaterial, null, 10000);
    }
}
