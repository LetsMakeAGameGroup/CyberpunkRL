using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IAbilityUser
{
    Transform UserTransform { get; }
}

public abstract class Ability : MonoBehaviour
{
    IAbilityUser abilityUser;

    public class OnAbilityUse : UnityEvent<float> { }
    public OnAbilityUse onAbilityUse = new OnAbilityUse();

    [Header("Ability Info")]
    public string abilityManaCost;
    public float abilityCooldownTime = 1;

    [SerializeField] AbilityData abilityData;
    private bool canUseAbility = true;

    public void InitializeAbility(IAbilityUser user)
    {
        abilityUser = user;
    }

    public void TriggerAbility()
    {
        if (!canUseAbility)
        {
            Debug.Log("Ability on Cooldown");
            return;
        }

        onAbilityUse.Invoke(abilityCooldownTime);
        PerformAbility();

        if (abilityCooldownTime > 0)
        {
            StartCooldown();
        }
    }

    public abstract void PerformAbility();
    public virtual void EndAbility() { }

    void StartCooldown()
    { 
        StartCoroutine(Cooldown());

        IEnumerator Cooldown()
        {
            canUseAbility = false;
            yield return new WaitForSeconds(abilityCooldownTime);
            canUseAbility = true;
        }
    }
}