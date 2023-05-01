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
        //Debug.Log("GoToTarget: " + targetString);
        Transform target = (Transform)GetData("target");
        if (target == null) {
            state = NodeState.FAILURE;
            return state;
        }

        //Debug.Log("GoToTarget target: " + target.name);
        if (Vector3.Distance(transform.position, target.position) > 0.01f) {
            transform.GetComponent<NavMeshAgent>().speed = EnemyBT.speed;
            transform.GetComponent<NavMeshAgent>().destination = target.position;
            //transform.position = Vector3.MoveTowards(transform.position, target.position, EnemyBT.speed * Time.deltaTime);
            //transform.LookAt(target.position);
        }

        state = NodeState.RUNNING;
        return state;
    }
}
