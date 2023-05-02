using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public abstract class Tree : MonoBehaviour {
        private Node root = null;

        public float speed = 10f;
        public float fovRange = 10f;

        public float attackRange = 2f;
        public int attackDamage = 5;

        public float spellRange = 4f;

        protected void Start() {
            root = SetupTree();
        }

        private void Update() {
            if (root != null) {
                root.Evaluate();
            }
        }

        protected abstract Node SetupTree();
    }
}