using BehaviorTree;
using System;
using System.Collections.Generic;

public class EnemyBT : Tree {
    public static float speed = 2f;
    public static float fovRange = 10f;

    public static float attackRange = 2f;
    public static int attackDamage = 5;

    public static float spellRange = 4f;
    public static int manaCost = 30;

    protected override Node SetupTree() {
        List<Node> list = new() {
            new Selector(new List<Node> {
                new CheckCastingSpell(transform),
                new Sequence(new List<Node> {
                    new CheckTargetInSpellRange(transform, UnityEngine.LayerMask.GetMask("Player")),
                    new CheckSufficientMana(transform),
                    new TaskCastSpellAtTarget(transform),
                }),
            }),
            new Selector(new List<Node> {
                new Sequence(new List<Node> {
                    new CheckTargetInAttackRange(transform),
                    new TaskAttackTarget(transform),
                }),
                new Sequence(new List<Node> {
                    new CheckTargetInFOVRange(transform, UnityEngine.LayerMask.GetMask("Player")),
                    new TaskGoToTarget(transform),
                }),
            }),
            new Selector(new List<Node> {
                new Sequence(new List<Node> {
                    new CheckTargetInAttackRange(transform),
                    new TaskAttackTarget(transform),
                }),
                new Sequence(new List<Node> {
                    new CheckTargetInFOVRange(transform, UnityEngine.LayerMask.GetMask("Objective")),
                    new TaskGoToTarget(transform),
                }),
            }),
            //new TaskPatrol(transform, waypoints),
        };
        Node root = new Selector(list);
        return root;
    }
}
