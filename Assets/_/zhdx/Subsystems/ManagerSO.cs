using UnityEngine;

namespace zhdx
{
    namespace Subsystems
    {
        public abstract class ManagerSO : ScriptableObject
        {
            public abstract bool InitializeOnEditMode();

            public abstract void Initialize();

            public bool AbleToInitialize() => InitializeOnEditMode() || Application.isPlaying;
        }
    }
}