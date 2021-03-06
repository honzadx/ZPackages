﻿using System.Collections.Generic;
using UnityEngine;

namespace zhdx
{
    namespace Subsystems
    {
        [ExecuteAlways]
        public class SOPackager : MonoBehaviour
        {
            private static SOPackager instance;
            public static SOPackager Instance => !Application.isPlaying ? FindObjectOfType<SOPackager>() : instance;

            public SceneManagement.SceneManagerSO sceneManager;

            public List<ManagerSO> managers;

            public void Awake()
            {
                if (instance == null && Application.isPlaying)
                {
                    instance = this;
                    DontDestroyOnLoad(transform);
                    sceneManager?.Initialize();
                }
                else if (instance != this && Application.isPlaying)
                {
                    Destroy(gameObject);
                    return;
                }

                for (int i = 0; i < managers.Count; ++i)
                {
                    managers[i].Initialize();
                }

                Debug.Log("SOPackager Initialized in " + (Application.isPlaying ? "PlayMode" : "EditMode"));
            }

            public void OnDisable()
            {
                if (instance == this)
                    instance = null;
            }
        }
    }
}
