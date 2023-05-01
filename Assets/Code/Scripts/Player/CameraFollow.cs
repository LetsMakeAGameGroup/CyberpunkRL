using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Update() {
        if (target != null) transform.position = target.position + new Vector3(0, 15, 0);
    }
}
