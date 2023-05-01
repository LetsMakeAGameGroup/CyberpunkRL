using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckTargetInAttackRange : Node
{
    private Transform transform;
    //private Animator animator;

    public CheckTargetInAttackRange(Transform _transform) {
        transform = _transform;
        //animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate() {
        Transform target = (Transform)GetData("target");
        if (target == null) {
            state = NodeState.FAILURE;
            return state;
        }

        if (Vector3.Distance(transform.position, target.position) <= EnemyBT.attackRange) {
            //animator.SetBool("Attacking", true);
            //animator.SetBool("Walking", false);

            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
