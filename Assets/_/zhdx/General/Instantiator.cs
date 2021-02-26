using System.Collections.Generic;
using UnityEngine;

namespace zhdx
{
    namespace General
    {
        public class Instantiator : MonoBehaviour
        {
            [System.Serializable]
            public struct PrefabLink
            {
                public string id;
                public GameObject prefab;
                public Transform parent;
            }

            [SerializeField]
            private List<PrefabLink> prefabLinks = null;

            public void Instantiate(string id)
            {
                for (int i = 0; i < prefabLinks.Count; i++)
                {
                    var prefabLink = prefabLinks[i];
                    if (prefabLink.id != id)
                        continue;
                    Instantiate(prefabLink.prefab, transform.position, transform.rotation, prefabLink.parent);
                }
            }
        }
    }
}