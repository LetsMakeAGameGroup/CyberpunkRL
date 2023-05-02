using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckCastingSpell : Node {
    private SpellBase spell;
    //private Animator animator;

    public CheckCastingSpell(Transform _transform) {
        if (!_transform.TryGetComponent(out spell)) {
            Debug.LogError("Could not retrieve SpellBase.", _transform);
        }
        //animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate() {
        if (spell.isCastingSpell) {
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
