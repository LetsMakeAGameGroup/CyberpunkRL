using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour {
    [SerializeField] private RectTransform manaRect;
    private float manaRectWidth = 1920f;

    [SerializeField] private int maxMana;
    private int currentMana;
    [SerializeField] private int manaPerSec;

    private float timeInterval = 0f;

    private void Awake() {
        currentMana = maxMana;
        manaRectWidth = manaRect.sizeDelta.x;
    }

    private void Update() {
        timeInterval += Time.deltaTime;
        if (timeInterval >= 1f) {
            timeInterval = 0f;
            currentMana += manaPerSec;
            if (currentMana > maxMana) {
                currentMana = maxMana;
            }
            manaRect.sizeDelta = new Vector2((manaRectWidth/maxMana)*currentMana, manaRect.sizeDelta.y);
        }
    }

    public void ConsumeMana(int mana) {
        currentMana -= mana;
        Debug.Log($"Mana is now{currentMana}/{maxMana}");
        manaRect.sizeDelta = new Vector2((manaRectWidth/maxMana)*currentMana, manaRect.sizeDelta.y);
    }

    public bool HasSufficientMana(int manaCost) {
        if (currentMana >= manaCost) {
            return true;
        }
        return false;
    }
}
