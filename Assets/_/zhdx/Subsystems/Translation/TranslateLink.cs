using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace zhdx.Subsystems.Translation
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

        #region ONPLAY_FUNCTIONS

        private void Start()
        {
            UpdateTexts(TranslationCatalogManagerSO.Instance);
        }

        private void OnEnable()
        {
            TranslationCatalogManagerSO.Instance.LanguageChanged += OnLanguageChanged;
        }

        private void OnDisable()
        {
            var translationManager = TranslationCatalogManagerSO.Instance;
            if (translationManager != null)
                translationManager.LanguageChanged -= OnLanguageChanged;
        }

        private void OnLanguageChanged()
        {
            if (this != null)
            {
                UpdateTexts(TranslationCatalogManagerSO.Instance);
            }
        }

        private void UpdateTexts(TranslationCatalogManagerSO translationManager)
        {
            foreach (TranslateText tT in translateTexts)
            {
                var text = translationManager.Translate(tT.id);
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
        internal void EditorUpdateTexts()
        {
            var translationManager = FindObjectOfType<TranslationCatalogManagerSO>();
            UpdateTexts(translationManager);
        }

        internal void EditorAdd()
        {
            if (translateTexts == null)
                translateTexts = new List<TranslateText>();

            translateTexts.Add(new TranslateText());
        }

        internal void EditorRemoveAt(int index)
        {
            translateTexts.RemoveAt(index);
        }
        #endregion // EDITOR_FUNCTIONS
    }
}

