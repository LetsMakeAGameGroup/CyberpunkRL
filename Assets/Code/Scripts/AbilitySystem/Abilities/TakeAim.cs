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
        SpawnProjectiles();
    }

    void SpawnProjectiles() 
    {
        
    }

    public override void EndAbility()
    {
        Debug.Log("End Take Aim Ability!");
        wantsToShoot = false;
    }
}
