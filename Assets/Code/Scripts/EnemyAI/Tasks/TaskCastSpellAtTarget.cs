using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine.UIElements;

public class TaskCastSpellAtTarget : Node {
    //private Animator animator;
    private Mana mana;
    private SpellBase spell;
    private NavMeshAgent agent;

    private Transform lastTarget;
    private Health health;

    private float attackTime = 5f;
    private float attackCounter = 0f;

    public TaskCastSpellAtTarget(Transform transform) {
        //animator = transform.GetComponent<Animator>();
        if (!transform.TryGetComponent(out mana)) {
            Debug.LogError("Could not retrieve Mana.", transform);
        }
        if (!transform.TryGetComponent(out spell)) {
            Debug.LogError("Could not retrieve SpellBase.", transform);
        }
        if (!transform.TryGetComponent(out agent)) {
            Debug.LogError("Could not retrieve NavMeshAgent.", transform);
        }
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

        if (agent.destination != null) {
            agent.ResetPath();
        }

        Debug.Log("Cast spell from BT");
        spell.InitiateSpellCast(target);

        state = NodeState.RUNNING;
        return state;
    }
}
