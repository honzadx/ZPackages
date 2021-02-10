using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zhdx.General
{
    public class ActiveLink : MonoBehaviour
    {
        [SerializeField]
        private GameObject follower = null;
        [SerializeField]
        private GameObject target = null;
        [SerializeField]
        private bool inverse = false;

        private void Update()
        {
            follower.SetActive(target.activeInHierarchy ^ inverse);
        }
    }
}
