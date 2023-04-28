using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability Data", menuName = "AbilitySystem/AbilityData", order = 1)]
public class AbilityData : ScriptableObject
{
    public Sprite abilityIcon;
    public string abilityName;
    [TextArea(3, 5)] public string abilityDescription;
    public string abilityManaCost;
    public float abilityCooldownTime = 1;
}