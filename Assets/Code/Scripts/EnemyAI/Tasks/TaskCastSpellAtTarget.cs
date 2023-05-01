using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;
using Unity.VisualScripting;

public class TaskCastSpellAtTarget : Node {
    private Animator animator;
    private Mana mana;
    private SpellBase spell;
    private NavMeshAgent agent;

    private Transform lastTarget;
    private Health health;

    private float attackTime = 5f;
    private float attackCounter = 0f;

    public TaskCastSpellAtTarget(Transform transform) {
        animator = transform.GetComponent<Animator>();
        mana = transform.GetComponent<Mana>();
        spell = transform.GetComponent<SpellBase>();
        agent = transform.GetComponent<NavMeshAgent>();
    }

    public override NodeState Evaluate() {
        Transform target = (Transform)GetData("target");
        if (target == null) {
            state = NodeState.FAILURE;
            return state;
        }

        if (target != lastTarget) {
            attackCounter = attackTime;
            lastTarget = target;

            //parent.SetData("spellEndPos", target);
        }

        if (agent.destination != null) agent.ResetPath();

        Debug.Log("Cast spell from BT");
        spell.InitiateSpellCast(target);

        state = NodeState.RUNNING;
        return state;
    }
}
