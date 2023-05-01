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
    Vector3 CursorPosition { get; }
}

public abstract class Ability : MonoBehaviour
{
    protected IAbilityUser abilityUser;
    public AbilityTargetType abilityTargetType;

    public GameObject abilityTargetingUI;

    public class OnAbilityUse : UnityEvent<float> { }
    public OnAbilityUse onAbilityUse = new OnAbilityUse();

    [Header("Ability Info")]
    public int abilityManaCost;
    public float abilityCooldownTime = 1;
    public float requireCastTime = 0;
    public bool canBeCancelled;

    [Space][Space][Space]

    public bool canBeRecasted;
    public int maxRecastTime;
    
    [Space][Space][Space]

    private int timesRecasted = 0;
    private bool canUseAbility = true;

    [SerializeField] AbilityData abilityData;

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

    protected void StartCastTime() 
    {
        StartCoroutine(CastTimer());

        IEnumerator CastTimer()
        {
            yield return new WaitForSeconds(requireCastTime);

        }
    }

    public virtual void CancelAbility() 
    {
        if (!canBeCancelled) { return; }
    }
}