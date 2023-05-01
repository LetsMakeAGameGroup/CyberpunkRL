using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {
    Transform target;

    private void Awake() {
        target = Camera.main.transform;    
    }

    private void LateUpdate() {
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }
}
