using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum AbilityTargetType 
{
    SingleTarget,
    Skillshot
}

public interface IAbilityUser
{
    Transform UserTransform { get; }
    bool CastingAbility { get; set; }
    Vector2 MousePosition { get; }
}

public abstract class Ability : MonoBehaviour
{
    protected IAbilityUser abilityUser;
    public AbilityTargetType abilityTargetType;

    public GameObject abilityTargetingUI;

    public class OnAbilityUse : UnityEvent<float> { }
    public OnAbilityUse onAbilityUse = new OnAbilityUse();

    [Header("Ability Info")]
    public string abilityManaCost;
    public float abilityCooldownTime = 1;

    [SerializeField] AbilityData abilityData;
    bool requireCharge;
    public int maxRecastTime;
    private int timesRecasted = 0;
    public bool canBeRecasted;
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

        if (canBeRecasted)
        {
            //If we reach max recast time, we start cooldown
            if (timesRecasted == maxRecastTime)
            {
                if (abilityCooldownTime > 0)
                {
                    StartCooldown();
                }
            }
        }
        else 
        {
            if (abilityCooldownTime > 0)
            {
                StartCooldown();
            }
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