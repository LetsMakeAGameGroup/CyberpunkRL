using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckSufficientMana : Node {
    private Transform transform;
    //private Animator animator;

    public CheckSufficientMana(Transform _transform) {
        transform = _transform;
        //animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate() {
        if (!transform.TryGetComponent(out Mana mana) || !transform.TryGetComponent(out SpellBase spell)) {
            state = NodeState.FAILURE;
            return state;
        }

        if (mana.HasSufficientMana(spell.manaCost)) {
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
