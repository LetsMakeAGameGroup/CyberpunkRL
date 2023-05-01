using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Thruster : SpellBase {
    [SerializeField] private float thrustSpeed = 1f;
    private bool alreadyHit = false;

    public override IEnumerator CastSpell() {
        //Vector3 dir = (transform.position - target.position).normalized;
        alreadyHit = false;

        Vector3 targetDir = target.position;
        targetDir.y = transform.position.y;
        transform.LookAt(targetDir);

        float castTime = 0;
        while (castTime < 1f) {
            transform.Translate(thrustSpeed * Time.deltaTime * Vector3.forward);
            castTime += Time.deltaTime;
            yield return null;
        }
        isCastingSpell = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player") || !isCastingSpell || alreadyHit) return;

        if (other.transform == target) {
            alreadyHit = true;
            target.GetComponent<Health>().TakeDamage(15);
        }
    }
}
