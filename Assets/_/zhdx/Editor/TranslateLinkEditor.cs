using UnityEngine;
using TMPro;
using zhdx.Subsystems.Translation;
using zhdx.Subsystems;
using zhdx;

namespace UnityEditor
{ 
    [CustomEditor(typeof(TranslateLink))]
    public class TranslateLinkEditor : Editor
    {
        private TranslateLink _target;
        private TranslationCatalogManagerSO tcm;

        private bool resetChecks;
        private bool setDirty;

        private void Awake()
        {
            resetChecks = true;
        }

        public override void OnInspectorGUI()
        {
            _target = (TranslateLink)target;

            GUILayout.Space(20);

            if(tcm == null)
            {
                if (SOPackager.Instance != null)
                {
                    SOPackager.Instance.Awake();
                    tcm = TranslationCatalogManagerSO.Instance;
                }
            }

            var translateTextsCount = _target.translateTexts != null ? _target.translateTexts.Count : 0;

            Rect body = EditorGUILayout.BeginVertical();
            EditorGUI.DrawRect(body, ZEditorStyles.ACTIVE_BOX_COLOR);

            Rect header = EditorGUILayout.BeginVertical();
            EditorGUI.DrawRect(header, ZEditorStyles.HEADER_TRANSLATION);
            GUI.contentColor = Color.black;
            GUILayout.Label("TRANSLATION LINK", EditorStyles.boldLabel);
            GUI.contentColor = Color.white;
            EditorGUILayout.EndVertical();

            
            GUILayout.BeginHorizontal();
            if(GUILayout.Button("REFRESH"))
            {
                Undo.RecordObject(_target, "(Translation) Updated catalog");
                SOPackager.Instance.Awake();
                resetChecks = true;
                setDirty = true;
            }
            if (GUILayout.Button("UPDATE TEXTS"))
            {
                _target.EditorUpdateTexts();
            }
            if (GUILayout.Button("+"))
            {
                Undo.RecordObject(_target, "(Translation) Added link");
                _target.EditorAdd();
                setDirty = true;
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(ZEditorStyles.LINE_HEIGHT);
            for(int i = translateTextsCount - 1; i >= 0; --i)
            {
                var translateText = _target.translateTexts[i];

                translateText = TranslateTextChecks(translateText);
                DrawTranslateText(translateText, i);
            }
            EditorGUILayout.EndVertical();

            if(setDirty) EditorUtility.SetDirty(_target);

            resetChecks = false;
            setDirty = false;
        }

        private TranslateLink.TranslateText TranslateTextChecks(TranslateLink.TranslateText translateText)
        {
            if (!translateText.idChecked || resetChecks)
            {
                translateText.keyExists = tcm.KeyExists(translateText.id);
                translateText.idChecked = true;
            }

            if (!translateText.typeChecked || resetChecks)
            {
                if (translateText.textObject != null && translateText.textObject.GetComponent<TextMeshProUGUI>())
                {
                    translateText.textType = UITextType.TextMeshProUGUI;
                }
                else if (translateText.textObject != null && translateText.textObject.GetComponent<TextMeshPro>())
                {
                    translateText.textType = UITextType.TextMeshPro;
                }
                else
                {
                    translateText.textType = UITextType.NONE;
                }
                translateText.typeChecked = true;
            }
            return translateText;
        }

        private TranslateLink.TranslateText DrawTranslateText(TranslateLink.TranslateText translateText, int i)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(ZEditorStyles.HORIZONTAL_HIERARCHY_SPACE);
            GUILayout.BeginVertical();
            Rect body = EditorGUILayout.BeginVertical();
            EditorGUI.DrawRect(body, translateText.keyExists && translateText.textObject != null ? ZEditorStyles.ACTIVE_BOX_COLOR : ZEditorStyles.ERROR_BOX_COLOR);
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Space(5);
            if (GUILayout.Button("X", GUILayout.Width(20f)))
            {
                Undo.RecordObject(_target, "(Translation) Removed link");
                _target.EditorRemoveAt(i);
                setDirty = true;
                GUILayout.EndHorizontal();
            }
            else
            {
                GUI.contentColor = translateText.keyExists ? Color.white : Color.red;
                var id = EditorGUILayout.TextField(translateText.id);
                GUI.contentColor = Color.white;
                GUILayout.EndHorizontal();
                if (id != translateText.id)
                {
                    Undo.RecordObject(_target, "(Translation) Changed link id");
                    translateText.id = id;
                    translateText.idChecked = false;
                    setDirty = true;
                }

                GUILayout.BeginHorizontal();
                var textObject = (GameObject)EditorGUILayout.ObjectField(translateText.textObject, typeof(GameObject), true);
                var enumPopup = EditorGUILayout.EnumPopup(translateText.textType);
                GUILayout.EndHorizontal();
                if (translateText.textObject != textObject)
                {
                    Undo.RecordObject(_target, "(Translation) Changed textObject");
                    translateText.textObject = textObject;
                    translateText.typeChecked = false;
                    setDirty = true;
                }

                _target.translateTexts[i] = translateText;
            }
            GUILayout.Space(5);
            EditorGUILayout.EndVertical();
            GUILayout.Space(5);
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
           
            
            return translateText;
        }
    }
}
