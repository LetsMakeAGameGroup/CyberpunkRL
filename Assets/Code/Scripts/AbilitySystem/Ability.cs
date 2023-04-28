using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Ability : MonoBehaviour
{
    public class OnAbilityUse : UnityEvent<float> { }
    public OnAbilityUse onAbilityUse = new OnAbilityUse();

    [Header("Ability Info")]
    [SerializeField] AbilityData abilityData;
    private bool canUseAbility = true;

    public void TriggerAbility()
    {
        if (!canUseAbility)
        {
            Debug.Log("Ability on Cooldown");
            return;
        }

        onAbilityUse.Invoke(abilityData.abilityCooldownTime);
        PerformAbility();
        StartCooldown();
    }

    public abstract void PerformAbility();
    public virtual void EndAbility() { }

    void StartCooldown()
    { 
        StartCoroutine(Cooldown());

        IEnumerator Cooldown()
        {
            canUseAbility = false;
            yield return new WaitForSeconds(abilityData.abilityCooldownTime);
            canUseAbility = true;
        }
    }
}