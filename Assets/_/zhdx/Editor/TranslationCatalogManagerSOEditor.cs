using UnityEngine;
using zhdx.Subsystems.Translation;

namespace UnityEditor
{
    [CustomEditor(typeof(TranslationCatalogManagerSO))]
    public class TranslationCatalogManagerSOEditor : Editor
    {
        private TranslationCatalogManagerSO _target;

        public override void OnInspectorGUI()
        {
            _target = (TranslationCatalogManagerSO)target;

            GUILayout.Space(20);

            Rect body = EditorGUILayout.BeginVertical();
            EditorGUI.DrawRect(body, ZEditorStyles.ACTIVE_BOX_COLOR);

            Rect header = EditorGUILayout.BeginHorizontal();
            EditorGUI.DrawRect(header, ZEditorStyles.HEADER_TRANSLATION);
            GUI.contentColor = Color.black;
            GUILayout.Label("TRANSLATION CATALOG MANAGER", EditorStyles.boldLabel);
            GUI.contentColor = Color.white;
            EditorGUILayout.EndHorizontal();

            var translationCatalog = (TranslationCatalogSO) EditorGUILayout.ObjectField("Translation catalog", _target.translationCatalog, typeof(TranslationCatalogSO), false);
            var language = _target.currentLanguage;
            if (translationCatalog != null)
            {
                language = translationCatalog.languages[EditorGUILayout.Popup("Current language", translationCatalog.GetLanguageIndex(language), translationCatalog.GetLanguages())];
            }
            GUILayout.Space(10);
            EditorGUILayout.EndVertical();

            if (translationCatalog != _target.translationCatalog || language != _target.currentLanguage)
            {
                Undo.RecordObject(_target, "(Translation) Catalog manager changed");
                _target.translationCatalog = translationCatalog;
                _target.currentLanguage = language;
                EditorUtility.SetDirty(_target);
            }
        }
    }

}
