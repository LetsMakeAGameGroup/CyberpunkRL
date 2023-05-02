using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spell_Barrier : SpellBase {
    [SerializeField] private float barrierTime = 5f;
    [SerializeField] private GameObject barrierPrefab = null;
    [SerializeField] private float damageReduction = 0.5f;

    public override IEnumerator CastSpell() {
        yield return new WaitForSeconds(1f);
        isCastingSpell = false;

        if (!target.TryGetComponent(out Health health)) {
            Debug.LogError("Could not retrive Health.", transform);
        }

        GameObject barrier = Instantiate(barrierPrefab, target);
        health.SetDamageTakenPerc(damageReduction);

        yield return new WaitForSeconds(barrierTime);

        Destroy(barrier);
        health.SetDamageTakenPerc(0f);
    }
}
