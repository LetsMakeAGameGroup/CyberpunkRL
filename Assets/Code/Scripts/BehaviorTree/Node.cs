using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace BehaviorTree {
    public enum NodeState {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public class Node {
        protected NodeState state;

        public Node parent;
        protected List<Node> children = new();

        private Dictionary<string, object> dataContext = new();

        public Node() {
            parent = null;
        }

        public Node(List<Node> children) {
            foreach (Node child in children) {
                Attach(child);
            }
        }

        // Attaches a node below the current node.
        private void Attach(Node node) {
            node.parent = this;
            children.Add(node);
        }

        // Evaluates the condition and returns its outcome.
        public virtual NodeState Evaluate() => NodeState.FAILURE;

        // Set data to the key provided.
        public void SetData(string key, object value) {
            dataContext[key] = value;
        }

        // Returns data from the key provided. If the key cannot be found, scale up the tree till it can find the key to return. Returns null if not found.
        public object GetData(string key) {
            if (dataContext.TryGetValue(key, out object value)) {
                return value;
            }

            Node node = parent;
            while (node != null) {
                value = node.GetData(key);
                if (value != null) {
                    return value;
                }
                node = node.parent;
            }
            return null;
        }

        // Clears data of the key provided. If the key cannot be found, scale up the tree till it can find the key to clear. Returns true if done successfully.
        public bool ClearData(string key) {
            if (dataContext.ContainsKey(key)) {
                dataContext.Remove(key);
                return true;
            }

            Node node = parent;
            while (node != null) {
                if (node.ClearData(key)) {
                    return true;
                }
                node = node.parent;
            }
            return false;
        }
    }
}
