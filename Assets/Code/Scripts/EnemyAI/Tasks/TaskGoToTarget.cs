using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;

public class TaskGoToTarget : Node
{
    private Transform transform;

    public TaskGoToTarget(Transform _transform) {
        transform = _transform;
    }

    public override NodeState Evaluate() {
        Transform target = (Transform)GetData("target");
        if (target == null) {
            state = NodeState.FAILURE;
            return state;
        }

        if (Vector3.Distance(transform.position, target.position) > transform.GetComponent<NavMeshAgent>().stoppingDistance) {
            transform.GetComponent<NavMeshAgent>().speed = transform.GetComponent<BehaviorTree.Tree>().speed;
            transform.GetComponent<NavMeshAgent>().destination = target.position;
        }

        state = NodeState.RUNNING;
        return state;
    }
}
