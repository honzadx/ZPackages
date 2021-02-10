using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zhdx.Subsystems
{
    public abstract class ManagerSO : ScriptableObject
    {
        public abstract bool InitializeOnEditMode();

        public abstract void Initialize();

        public bool AbleToInitialize() => InitializeOnEditMode() || Application.isPlaying;
    }
}