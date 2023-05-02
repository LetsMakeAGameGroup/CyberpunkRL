using UnityEngine;
using BehaviorTree;

public class CheckTargetInFOVRange : Node {
    private LayerMask objectLayerMask;

    private Transform transform;

    private BehaviorTree.Tree tree;
    //private Animator animator;

    public CheckTargetInFOVRange(Transform _transform, LayerMask _objectLayerMask) {
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
                float distanceClosest = Vector3.Distance(transform.position, colliders[0].transform.position);
                for (int i = 1; i < colliders.Length; i++) {
                    if (Vector3.Distance(transform.position, colliders[i].transform.position) < distanceClosest) {
                        closestCollider = i;
                    }
                }
            }
            parent.parent.SetData("target", colliders[closestCollider].transform);
            //animator.SetBool("Walking", true);

            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
