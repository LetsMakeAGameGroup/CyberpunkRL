using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(NavMeshAgent))]
public class InstantTurnMovement : MonoBehaviour {
    private NavMeshAgent agent;
    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }

    private void LateUpdate() {
        if (agent.velocity.sqrMagnitude > Mathf.Epsilon) {
            transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        if (!transform.TryGetComponent(out BehaviorTree.Tree tree)) {
            Debug.LogError("Could not retrieve Tree.", transform);
        }
        Gizmos.DrawWireSphere(transform.position, tree.fovRange);
    }
}
