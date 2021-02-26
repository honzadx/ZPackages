using UnityEngine;

namespace zhdx
{
    namespace Subsystems
    {
        namespace SceneManagement
        {
            public class LoadScene : MonoBehaviour
            {
                [SerializeField]
                private string scene = "";

                public void Load()
                {
                    SceneManagerSO.Instance.Load(scene);
                }
            }
        }
    }
}
