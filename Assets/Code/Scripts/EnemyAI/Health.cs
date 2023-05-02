using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] private RectTransform healthRect;
    private float healthRectWidth = 1920f;

    [SerializeField] private float maxHealth;
    private float currentHealth;

    [SerializeField] private float damageTaken = 1f;  // 1 means you take the entire damage while 0 means you take no damage

    private void Awake() {
        currentHealth = maxHealth;
        healthRectWidth = healthRect.sizeDelta.x;
    }

    public bool TakeDamage(float damage) {
        currentHealth -= (damage * damageTaken);
        if (currentHealth <= 0) {
            Die();
            Debug.Log("Player is now dead");
            healthRect.sizeDelta = new Vector2(0, healthRect.sizeDelta.y);
            return true;
        }
        Debug.Log("Player is now at " + currentHealth + " health.");
        healthRect.sizeDelta = new Vector2((healthRectWidth/maxHealth)*currentHealth, healthRect.sizeDelta.y);
        return false;
    }

    private void Die() {
        Destroy(gameObject);
    }

    public void SetDamageTakenPerc(float damageReduction) {
        damageTaken = 1f - damageReduction;
    }
}
