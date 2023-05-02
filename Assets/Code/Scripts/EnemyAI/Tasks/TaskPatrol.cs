using UnityEngine;
using UnityEngine.AI;

using BehaviorTree;

public class TaskPatrol : Node {
    private Transform transform;
    //private Animator animator;
    private Transform[] waypoints;

    private int currentWaypointIndex = 0;

    private float waitTime = 0.1f;
    private float waitCounter = 0f;
    private bool waiting = false;
    
    public TaskPatrol(Transform _transform, Transform[] _waypoints) {
        transform = _transform;
        //animator = transform.GetComponent<Animator>();
        waypoints = _waypoints;
    }

    public override NodeState Evaluate() {
        if (waypoints == null || waypoints.Length == 0) {
            state = NodeState.FAILURE;
            return state;
        }

        if (waiting) {
            waitCounter += Time.deltaTime;
            if (waitCounter >= waitTime) {
                waiting = false;
                //animator.SetBool("Walking", true);
            }
        } else {
            Vector3 wp = waypoints[currentWaypointIndex].position;
            wp.y = transform.position.y;
            if (Vector3.Distance(transform.position, wp) < 0.01f) {
                transform.position = wp;
                waitCounter = 0f;
                waiting = true;

                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                //animator.SetBool("Walking", false);
            } else {
                transform.GetComponent<NavMeshAgent>().speed = transform.GetComponent<BehaviorTree.Tree>().speed;
                transform.GetComponent<NavMeshAgent>().destination = wp;
                //transform.position = Vector3.MoveTowards(transform.position, wp.position, transform.GetComponent<BehaviorTree.Tree>().speed * Time.deltaTime);
                //transform.LookAt(wp.position);
            }
        }

        state = NodeState.RUNNING;
        return state;
    }
}
