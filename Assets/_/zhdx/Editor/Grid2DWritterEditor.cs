using UnityEngine;
using zhdx.Grid2D;

namespace UnityEditor
{
    [CustomEditor(typeof(Grid2DWritter))]
    public class Grid2DWritterEditor : Editor
    {
        private Grid2DWritter _target;

        private int value;
        private int x;
        private int y;

        private bool setDirty;

        public override void OnInspectorGUI()
        {
            _target = (Grid2DWritter)target;

            DrawRaycast();
            GUILayout.Space(ZEditorStyles.LINE_HEIGHT);
            DrawInEditor();
            
            if (setDirty)
                EditorUtility.SetDirty(_target);
        }

        public void DrawRaycast()
        {
            Rect header = EditorGUILayout.BeginHorizontal();
            EditorGUI.DrawRect(header, Color.black);
            GUILayout.Label("RAYCAST", EditorStyles.boldLabel);
            var raycastEdit = EditorGUILayout.Toggle(_target.raycastEdit, GUILayout.Width(20));
            EditorGUILayout.EndHorizontal();

            Rect body = EditorGUILayout.BeginVertical();
            EditorGUI.DrawRect(body, ZEditorStyles.ACTIVE_BOX_COLOR);
            var raycastValue = EditorGUILayout.IntField(_target.raycastValue);

            if (raycastEdit != _target.raycastEdit || raycastValue != _target.raycastValue)
            {
                _target.raycastEdit = raycastEdit;
                _target.raycastValue = raycastValue;
                setDirty = true;
            }
            EditorGUILayout.EndVertical();

        }

        public void DrawInEditor()
        {
            Rect header = EditorGUILayout.BeginHorizontal();
            EditorGUI.DrawRect(header, Color.black);
            GUILayout.Label("IN EDITOR", EditorStyles.boldLabel);
            EditorGUILayout.EndHorizontal();

            Rect body = EditorGUILayout.BeginVertical();
            EditorGUI.DrawRect(body, ZEditorStyles.ACTIVE_BOX_COLOR);
            GUILayout.BeginHorizontal();
            GUILayout.Label("X", GUILayout.Width(20));
            x = EditorGUILayout.IntField(x);
            GUILayout.Label("Y", GUILayout.Width(20));
            y = EditorGUILayout.IntField(y);
            GUILayout.EndHorizontal();
            value = EditorGUILayout.IntField(value);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Set"))
            {
                _target.Set(x, y, value);
                EditorUtility.SetDirty(_target.grid);
            }
            if (GUILayout.Button("Clear"))
            {
                _target.Clear(x, y);
                EditorUtility.SetDirty(_target.grid);
            }
            GUILayout.EndHorizontal();
            if (GUILayout.Button("Clear All"))
            {
                _target.ClearAll();
                EditorUtility.SetDirty(_target.grid);
            }
            EditorGUILayout.EndVertical();

        }
    }
}