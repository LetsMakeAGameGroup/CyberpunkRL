using BehaviorTree;
using System;
using System.Collections.Generic;

public class PlayerBT : Tree {
    public UnityEngine.Transform[] waypoints;

    public static float speed = 8f;
    public static float fovRange = 10f;

    public static float attackRange = 2f;
    public static int attackDamage = 5;

    public static float spellRange = 4f;
    public static int manaCost = 30;

    protected override Node SetupTree() {
        List<Node> list = new() {
            new TaskPatrol(transform, waypoints),
        };
        Node root = new Selector(list);
        return root;
    }
}
