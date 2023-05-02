using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour {
    [SerializeField] private RectTransform manaRect;
    private float manaRectWidth = 1920f;

    [SerializeField] private float maxMana;
    private float currentMana;

    private void Awake() {
        currentMana = maxMana;
        manaRectWidth = manaRect.sizeDelta.x;
    }

    private void Update() {
        if (currentMana < maxMana) {
            currentMana += Time.deltaTime;
            if (currentMana > maxMana) {
                currentMana = maxMana;
            }
            manaRect.sizeDelta = new Vector2((manaRectWidth/maxMana)*currentMana, manaRect.sizeDelta.y);
        }
    }

    public void ConsumeMana() {
        currentMana = 0;
        Debug.Log($"Mana is now {currentMana}/{maxMana}");
        manaRect.sizeDelta = new Vector2(0, manaRect.sizeDelta.y);
    }

    public bool HasSufficientMana() {
        if (currentMana == maxMana) {
            return true;
        }
        return false;
    }
}
