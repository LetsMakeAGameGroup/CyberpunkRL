using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;

public class TaskAttackTarget : Node
{
    private Animator animator;
    private NavMeshAgent agent;

    private Transform lastTarget;
    private Health health;

    private float attackTime = 1f;
    private float attackCounter = 0f;

    public TaskAttackTarget(Transform transform) {
        if (!transform.TryGetComponent(out animator)) {
            Debug.LogError("Could not retrieve Animator.", transform);
        }
        if (!transform.TryGetComponent(out agent)) {
            Debug.LogError("Could not retrieve NavMeshAgent.", transform);
        }
    }

    public override NodeState Evaluate() {
        Transform target = (Transform)GetData("target");

        if (target != lastTarget) {
            health = target.GetComponent<Health>();
            lastTarget = target;
        }

        if (agent.destination != null) agent.ResetPath();

        attackCounter += Time.deltaTime;
        if (attackCounter >= attackTime) {
            bool targetIsDead = health.TakeDamage(EnemyBT.attackDamage);
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
