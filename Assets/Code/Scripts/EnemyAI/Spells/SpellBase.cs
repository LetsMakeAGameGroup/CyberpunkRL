using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mana))]
public abstract class SpellBase : MonoBehaviour {
    public int manaCost = 1;

    public float cooldown = 1f;
    [HideInInspector] public float currentTimer = 0f;

    [HideInInspector] public bool isCastingSpell = false;

    protected Transform target;

    private void Update() {
        if (currentTimer < cooldown) {
            currentTimer += Time.deltaTime;
        }
    }

    public void InitiateSpellCast(Transform _target) {
        GetComponent<Mana>().ConsumeMana(manaCost);
        isCastingSpell = true;
        target = _target;
        StartCoroutine(CastSpell());
    }

    public abstract IEnumerator CastSpell();
}
