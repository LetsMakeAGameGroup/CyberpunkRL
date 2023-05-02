using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using Unity.VisualScripting;

public class CheckTargetInSpellRange : Node {
    private LayerMask objectLayerMask;

    private Transform transform;

    private BehaviorTree.Tree tree;
    //private Animator animator;

    public CheckTargetInSpellRange(Transform _transform, LayerMask _objectLayerMask) {
        transform = _transform;
        objectLayerMask = _objectLayerMask;
        if (!_transform.TryGetComponent(out tree)) {
            Debug.LogError("Could not retrieve Tree.", _transform);
        }
        //animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, tree.fovRange, objectLayerMask);

        if (colliders.Length > 0) {
            int closestCollider = 0;
            if (colliders.Length != 1) {
                float distanceClosest = (colliders[0].transform == transform) ? Mathf.Infinity : Vector3.Distance(transform.position, colliders[0].transform.position);
                for (int i = 1; i < colliders.Length; i++) {
                    if (colliders[i].transform == transform) continue;

                    if (Vector3.Distance(transform.position, colliders[i].transform.position) < distanceClosest) {
                        closestCollider = i;
                    }
                }
            }
            parent.SetData("target", colliders[closestCollider].transform);
            //animator.SetBool("Walking", true);

            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
