using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace zhdx.Subsystems.Translation
{
    [CustomEditor(typeof(TranslationCatalogSO))]
    public class TranslationCatalogSOEditor : Editor
    {
        private TranslationCatalogSO _target;

        private bool editLanguages = false;
        private bool editTranslations = false;

        private string newLang;
        private string newTranslation;

        public override void OnInspectorGUI()
        {
            _target = (TranslationCatalogSO)target;
            _target.EditorLazyInstantiate();

            GUILayout.Space(20);

            DrawLanguages();
            GUILayout.Space(ZEditorStyles.LINE_HEIGHT);
            DrawTranslations();

            GUILayout.Space(20);

            GUILayout.BeginHorizontal();
            if(GUILayout.Button("EXPORT CSV"))
            {
                _target.EditorExportCSV();
            }
            if (GUILayout.Button("IMPORT CSV"))
            {
                _target.EditorImportCSV();
            }
            GUILayout.EndHorizontal();

            EditorUtility.SetDirty(_target);
        }

        private void DrawLanguages()
        {

            Rect body = EditorGUILayout.BeginVertical();
            EditorGUI.DrawRect(body, editLanguages ? ZEditorStyles.ACTIVE_BOX_COLOR : ZEditorStyles.INACTIVE_BOX_COLOR);

            Rect header = EditorGUILayout.BeginHorizontal();
            EditorGUI.DrawRect(header, ZEditorStyles.HEADER_TRANSLATION);
            GUI.contentColor = Color.black;
            GUILayout.Label("LANGUAGES", EditorStyles.boldLabel);
            GUI.contentColor = Color.white;
            if (editLanguages)
            {
                if (GUILayout.Button("X", GUILayout.Width(50)))
                {
                    editLanguages = false;
                }
            }
            else
            {
                if (GUILayout.Button("EDIT", GUILayout.Width(50)))
                {
                    editLanguages = true;
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUI.BeginDisabledGroup(!editLanguages);
            GUILayout.BeginHorizontal();
            GUILayout.Space(ZEditorStyles.HORIZONTAL_HIERARCHY_SPACE);
            newLang = EditorGUILayout.TextField(newLang);
            if (GUILayout.Button("+", GUILayout.Width(50)))
            {
                Undo.RecordObject(_target, "(Translation) Added new language");
                
                _target.EditorAddLanguage(newLang);
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(ZEditorStyles.LINE_HEIGHT);

            
            for (int i = _target.languages.Count - 1; i >= 0 ; --i)
            {
                DrawLanguageRow(i);
            }

            EditorGUILayout.EndVertical();

            EditorGUI.EndDisabledGroup();
        }

        private void DrawLanguageRow(int index)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(ZEditorStyles.HORIZONTAL_HIERARCHY_SPACE);
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                Undo.RecordObject(_target, "(Translation) Removed language " + _target.languages[index]);

                _target.EditorRemoveLanguageAt(index);
            }
            else
            {
                GUILayout.Label(_target.languages[index], EditorStyles.boldLabel);
            }
            GUILayout.EndHorizontal();
        }

        private void DrawTranslations()
        {
            Rect body = EditorGUILayout.BeginVertical();
            EditorGUI.DrawRect(body, editTranslations ? ZEditorStyles.ACTIVE_BOX_COLOR : ZEditorStyles.INACTIVE_BOX_COLOR);

            Rect header = EditorGUILayout.BeginHorizontal();
            EditorGUI.DrawRect(header, ZEditorStyles.HEADER_TRANSLATION);
            GUI.contentColor = Color.black;
            GUILayout.Label("TRANSLATIONS", EditorStyles.boldLabel);
            GUI.contentColor = Color.white;
            if (editTranslations)
            {
                if (GUILayout.Button("X", GUILayout.Width(50)))
                {
                    editTranslations = false;
                }
            }
            else
            {
                if (GUILayout.Button("EDIT", GUILayout.Width(50)))
                {
                    editTranslations = true;
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUI.BeginDisabledGroup(!editTranslations);
            GUILayout.BeginHorizontal();
            GUILayout.Space(ZEditorStyles.HORIZONTAL_HIERARCHY_SPACE);
            newTranslation = EditorGUILayout.TextField(newTranslation);
            if (GUILayout.Button("+", GUILayout.Width(50)))
            {
                Undo.RecordObject(_target, "(Translation) Added new translation");
                _target.EditorAddTranslation(newTranslation);
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(ZEditorStyles.LINE_HEIGHT);

            for (int i = _target.translations.Count - 1; i >= 0; --i)
            {
                DrawTranslationRow(i);
            }

            EditorGUI.EndDisabledGroup();

            EditorGUILayout.EndVertical();
        }

        private void DrawTranslationRow(int index)
        {
            var translation = _target.translations[index];

            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Space(ZEditorStyles.HORIZONTAL_HIERARCHY_SPACE);

            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                Undo.RecordObject(_target, "(Translation) Removed translation " + _target.translations[index].id);
                _target.EditorRemoveTranslationAt(index);
            }
            else
            {
                GUILayout.Label(translation.id, EditorStyles.boldLabel);

                if (GUILayout.Button("↑", GUILayout.Width(20)))
                {
                    Undo.RecordObject(_target, $"(Translation) Moved translation {_target.translations[index].id}");
                    _target.EditorInsertTranslation(translation, index + 1);
                }
                if (GUILayout.Button("↓", GUILayout.Width(20)))
                {
                    Undo.RecordObject(_target, $"(Translation) Moved translation {_target.translations[index].id}");
                    _target.EditorInsertTranslation(translation, index - 1);
                }
            }
            GUILayout.EndHorizontal();

            for (int k = translation.languageTexts.Count - 1; k >= 0 ; --k)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(ZEditorStyles.HORIZONTAL_HIERARCHY_SPACE);
                var languageText = translation.languageTexts[k];
                GUILayout.Label(languageText.language, EditorStyles.boldLabel, GUILayout.Width(50));
                languageText.text = EditorGUILayout.TextField(languageText.text);

                translation.languageTexts[k] = languageText;
                GUILayout.EndHorizontal();
            }
        }
    }
}