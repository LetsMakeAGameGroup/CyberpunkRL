using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckTargetInSpellRange : Node {
    private LayerMask objectLayerMask;

    private Transform transform;
    //private Animator animator;

    public CheckTargetInSpellRange(Transform _transform, LayerMask _objectLayerMask) {
        transform = _transform;
        objectLayerMask = _objectLayerMask;
        //animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, EnemyBT.spellRange, objectLayerMask);

        if (colliders.Length > 0) {
            parent.SetData("target", colliders[0].transform);
            //animator.SetBool("Walking", true);

            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
