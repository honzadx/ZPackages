using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace zhdx.Subsystems.SceneManagement
{
    [CreateAssetMenu(menuName = "zhdx/Scene/Scene Manager")]
    public class SceneManagerSO : ManagerSO
    {
        public static SceneManagerSO Instance;

        public string initializationScene = "Initialization";
        public string firstScene = "Main";

        private string lastScene;
        private string currentScene;

        private int activeIndex;

        public override void Initialize()
        {
            if (AbleToInitialize())
            {
                if (initializationScene != SceneManager.GetSceneAt(0).name)
                {
                    LoadInitializationScene();
                }
                else
                {
                    LoadFirstScene();
                }
                Instance = this;
            }
        }

        public override bool InitializeOnEditMode() => false;

        private void LoadInitializationScene()
        {
            currentScene = SceneManager.GetSceneAt(0).name;
            lastScene = SceneManager.GetSceneAt(0).name;
            Load(initializationScene, false);
            activeIndex = 0;
        }

        private void LoadFirstScene()
        {
            currentScene = firstScene;
            lastScene = firstScene;
            Load(firstScene);
            activeIndex = 1;
        }

        public void Load(string name, bool active = true, bool track = true)
        {
            Debug.Log($"Load scene '{name}'");
            if (SceneManager.sceneCount >= 2)
            {
                Unload(currentScene);
                if(track) lastScene = currentScene;
            }

            AsyncOperation load = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
            load.completed += FinishedLoading;

            if(active) currentScene = name;
        }

        public void Reload()
        {
            Load(currentScene, true, false);
        }

        public void LoadLast()
        {
            Load(lastScene);
        }

        public void Unload(string name)
        {
            Debug.Log($"Unload scene '{name}'");
            SceneManager.UnloadSceneAsync(name);
        }

        public void FinishedLoading(AsyncOperation handler)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneAt(activeIndex));
            activeIndex = 1;
        }
    }
}

