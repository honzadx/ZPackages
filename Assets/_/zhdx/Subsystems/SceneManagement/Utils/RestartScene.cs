using UnityEngine;

namespace zhdx
{
    namespace Subsystems
    {
        namespace SceneManagement
        {
            public class RestartScene : MonoBehaviour
            {
                public void Load()
                {
                    SceneManagerSO.Instance.Reload();
                }
            }
        }
    }
}