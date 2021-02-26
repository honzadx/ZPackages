using UnityEngine;

namespace zhdx
{
    namespace Subsystems
    {
        namespace SceneManagement
        {
            public class LastScene : MonoBehaviour
            {
                public void Load()
                {
                    SceneManagerSO.Instance.LoadLast();
                }
            }
        }
    }
}

