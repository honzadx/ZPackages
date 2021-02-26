using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace zhdx
{
    namespace Subsystems
    {
        namespace Translation
        {
            public class TranslateLink : MonoBehaviour
            {
                [System.Serializable]
                public struct TranslateText
                {
                    public string id;
                    public GameObject textObject;
                    public UITextType textType;
                    public bool idChecked;
                    public bool typeChecked;
                    public bool keyExists;
                }

                public List<TranslateText> translateTexts = null;

                public TranslationCatalogManagerSO translationCatalogManager;

                #region ONPLAY_FUNCTIONS

                private void Start()
                {
                    StartCoroutine(Fetch());
                }

                private IEnumerator Fetch()
                {
                    while(TranslationCatalogManagerSO.Instance == null)
                    {
                        yield return null;
                    }
                    translationCatalogManager = TranslationCatalogManagerSO.Instance;
                    translationCatalogManager.LanguageChanged += OnLanguageChanged;
                    UpdateTexts();
                }

                private void OnDisable()
                {
                    if (translationCatalogManager != null)
                        translationCatalogManager.LanguageChanged -= OnLanguageChanged;
                }

                private void OnLanguageChanged()
                {
                    if (this != null)
                    {
                        UpdateTexts();
                    }
                }

                private void UpdateTexts(bool editor = false)
                {
                    for (int i = 0; i < translateTexts.Count; ++i)
                    {
                        var tT = translateTexts[i];
                        var text = translationCatalogManager.Translate(tT.id);
                        switch (tT.textType)
                        {
                            case UITextType.TextMeshProUGUI:
                                tT.textObject.GetComponent<TextMeshProUGUI>().text = text;
                                break;
                            case UITextType.TextMeshPro:
                                tT.textObject.GetComponent<TextMeshPro>().text = text;
                                break;
                        }
                    }
                }

                #endregion //ONPLAY_FUNCTIONS

                #region EDITOR_FUNCTIONS
#if UNITY_EDITOR
                public void EditorUpdateTexts()
                {
                    var guids = EditorGetManagerGUIDs();
                    if (guids == null || guids.Length == 0)
                        return;

                    var guid = guids[0];
                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    translationCatalogManager = (TranslationCatalogManagerSO)AssetDatabase.LoadAssetAtPath(path, typeof(TranslationCatalogManagerSO));
                    UpdateTexts(true);
                    translationCatalogManager = null;
                }

                public void EditorAdd()
                {
                    if (translateTexts == null)
                        translateTexts = new List<TranslateText>();

                    translateTexts.Add(new TranslateText());
                }

                public void EditorRemoveAt(int index)
                {
                    translateTexts.RemoveAt(index);
                }

                public string[] EditorGetManagerGUIDs()
                {
                    var assetSearch = "t:TranslationCatalogManagerSO";
                    return AssetDatabase.FindAssets(assetSearch);
                }
#endif
                #endregion // EDITOR_FUNCTIONS
            }
        }
    }
}