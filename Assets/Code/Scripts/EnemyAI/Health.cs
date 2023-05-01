using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] private RectTransform healthRect;
    private float healthRectWidth = 1920f;

    [SerializeField] private int maxHealh;
    private int currentHealth;

    private void Awake() {
        currentHealth = 100;
        healthRectWidth = healthRect.sizeDelta.x;
    }

    public bool TakeDamage(int damage) {
        currentHealth -= damage;
        if (currentHealth <= 0) {
            Die();
            Debug.Log("Player is now dead");
            healthRect.sizeDelta = new Vector2(0, healthRect.sizeDelta.y);
            return true;
        }
        Debug.Log("Player is now at " + currentHealth + " health.");
        healthRect.sizeDelta = new Vector2((healthRectWidth/maxHealh)*currentHealth, healthRect.sizeDelta.y);
        return false;
    }

    private void Die() {
        Destroy(gameObject);
    }
}
