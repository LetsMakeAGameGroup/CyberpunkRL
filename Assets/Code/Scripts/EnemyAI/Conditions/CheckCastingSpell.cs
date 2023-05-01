using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckCastingSpell : Node {
    private SpellBase spell;
    //private Animator animator;

    public CheckCastingSpell(Transform _transform) {
        spell = _transform.GetComponent<SpellBase>();
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
