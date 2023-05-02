using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine.UIElements;

public class TaskAttackTarget : Node
{
    private Transform transform;
    //private Animator animator;
    private NavMeshAgent agent;

    private Transform lastTarget;
    private Health health;

    private float attackTime = 1f;
    private float attackCounter = 0f;

    private BehaviorTree.Tree tree;

    public TaskAttackTarget(Transform _transform) {
        transform = _transform;
        //if (!transform.TryGetComponent(out animator)) {
        //    Debug.LogError("Could not retrieve Animator.", transform);
        //}
        if (!transform.TryGetComponent(out agent)) {
            Debug.LogError("Could not retrieve NavMeshAgent.", transform);
        }
        if (!_transform.TryGetComponent(out tree)) {
            Debug.LogError("Could not retrieve Tree.", _transform);
        }
    }

    public override NodeState Evaluate() {
        Transform target = (Transform)GetData("target");

        if (target != lastTarget) {
            if (!target.TryGetComponent(out Health _health)) {
                Debug.LogError("Could not retrieve Health.", target);
            }
            health = _health;
            lastTarget = target;
        }

        if (agent.destination != null) {
            agent.ResetPath();
        }

        attackCounter += Time.deltaTime;
        if (attackCounter >= attackTime) {
            bool targetIsDead = health.TakeDamage(tree.attackDamage);
            if (targetIsDead) {
                ClearData("target");
                //animator.SetBool("Attacking", false);
                //animator.SetBool("Walking", true);
            }
            attackCounter = 0f;
        }

        state = NodeState.RUNNING;
        return state;
    }
}
