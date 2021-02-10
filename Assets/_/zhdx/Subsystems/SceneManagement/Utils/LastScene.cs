using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zhdx.Subsystems.SceneManagement
{
    public class LastScene : MonoBehaviour
    {
        public void Load()
        {
            SceneManagerSO.Instance.LoadLast();
        }
    }
}

