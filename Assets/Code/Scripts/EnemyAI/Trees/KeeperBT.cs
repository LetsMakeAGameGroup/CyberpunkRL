using BehaviorTree;
using System;
using System.Collections.Generic;

public class KeeperBT : Tree {
    protected override Node SetupTree() {
        List<Node> list = new() {
            new Selector(new List<Node> {
                new CheckCastingSpell(transform),
                new Sequence(new List<Node> {
                    new CheckTargetInSpellRange(transform, UnityEngine.LayerMask.GetMask("Enemy")),
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
