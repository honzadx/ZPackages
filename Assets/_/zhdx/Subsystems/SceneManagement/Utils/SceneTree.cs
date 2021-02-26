using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace zhdx
{
    namespace Subsystems
    {
        namespace SceneManagement
        {
            public class SceneTree : MonoBehaviour
            {
                [SerializeField]
                private Button prefab = null;
                [SerializeField]
                private Transform target = null;

                private void Start()
                {
                    int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
                    for (int i = 1; i < sceneCount; i++)
                    {
                        var name = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
                        var button = Instantiate(prefab, target);
                        button.GetComponentInChildren<TextMeshProUGUI>().text = name;
                        button.onClick.AddListener(() => SceneManagerSO.Instance.Load(name));
                    }
                }
            }
        }
    }
}