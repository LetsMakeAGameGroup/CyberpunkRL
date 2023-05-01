using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAim : Ability
{
    public GameObject bulletPrefab;
    bool wantsToShoot;

    public override void PerformAbility()
    {
        Debug.Log("Take Aim Ability Used!");

        ShootTakeAimProjectile();
    }

    void ShootTakeAimProjectile()
    {
        Vector3 aimDir = abilityUser.CursorPosition - abilityUser.UserTransform.position;

        Debug.DrawLine(abilityUser.UserTransform.position, abilityUser.CursorPosition, Color.magenta);

        Rigidbody bullet = Instantiate(bulletPrefab, abilityUser.UserTransform.position, Quaternion.LookRotation(aimDir, Vector3.up), null).GetComponent<Rigidbody>();
        bullet.AddForce(bullet.transform.forward * 1000);

    }

    public override void EndAbility()
    {
        Debug.Log("End Take Aim Ability!");
        wantsToShoot = false;
    }
}
