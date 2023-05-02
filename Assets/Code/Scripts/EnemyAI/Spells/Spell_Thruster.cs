using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spell_Thruster : SpellBase {
    [SerializeField] private float thrustSpeed = 1f;
    private bool alreadyHit = false;

    public override IEnumerator CastSpell() {
        alreadyHit = false;

        Vector3 targetDir = target.position;
        targetDir.y = transform.position.y;
        transform.LookAt(targetDir);

        float castTime = 0;
        while (castTime < 1f) {
            transform.Translate(thrustSpeed * Time.deltaTime * Vector3.forward);
            castTime += Time.deltaTime;
            // TODO: Check if clashing with wall
            yield return null;
        }
        isCastingSpell = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player") || !isCastingSpell || alreadyHit) return;

        if (other.transform == target) {
            alreadyHit = true;
            if (!target.TryGetComponent(out Health health)) {
                Debug.LogError("Could not retrieve Health.", transform);
            }
            health.TakeDamage(30);
        }
    }
}
