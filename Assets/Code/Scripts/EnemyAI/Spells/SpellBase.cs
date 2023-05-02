using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mana))]
public abstract class SpellBase : MonoBehaviour {
    [HideInInspector] public bool isCastingSpell = false;

    protected Transform target;

    public void InitiateSpellCast(Transform _target) {
        GetComponent<Mana>().ConsumeMana();
        isCastingSpell = true;
        target = _target;
        StartCoroutine(CastSpell());
    }

    public abstract IEnumerator CastSpell();
}
