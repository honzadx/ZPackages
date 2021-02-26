using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zhdx
{
    namespace General
    {
        [RequireComponent(typeof(ActionEvent))]
        public class InteractionTrigger : MonoBehaviour
        {
            private ActionEvent ae;

            private void Start()
            {
                ae = GetComponent<ActionEvent>();
            }

            private void OnTriggerEnter(Collider other)
            {
                Debug.Log($"'{gameObject.name}': ActionEvent called via Trigger hit from '{other.gameObject.name}'!");
                ae.action?.Invoke();
            }
        }
    }
}

